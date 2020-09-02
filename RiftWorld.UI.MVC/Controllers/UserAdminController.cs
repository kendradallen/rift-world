using RiftWorld.UI.MVC.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RiftWorld.DATA.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace RiftWorld.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller
    {
        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            //added to make details full
            RiftWorldEntities db = new RiftWorldEntities();
            var userDeets = await db.UserDetails.Where(x => x.UserId == id).FirstAsync();
            ViewBag.CurrentCharacter = await db.Characters.Where(x => x.CharacterId == userDeets.CurrentCharacterId).Select(x => x.CharacterName).FirstOrDefaultAsync();
            ViewBag.PastCharacters = db.Characters.Where(x => x.PlayerId == id && x.IsRetired == true).Select(x => x.CharacterName).ToList();
            EditUserViewModel2 daUser = new EditUserViewModel2 { Id = user.Id, Email = user.Email, DiscordName = userDeets.DiscordName, DiscordDiscriminator = userDeets.DiscordDiscriminator, IsApproved = userDeets.IsApproved, ConsentFileName = userDeets.ConsentFileName, CurrentCharacterId = userDeets.CurrentCharacterId };
            return View(daUser);
        }

        //
        // GET: /Users/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                string discordFull = $"{userViewModel.DiscordName}#{userViewModel.DiscordDiscriminator:D4}";
                var user = new ApplicationUser { UserName = discordFull, Email = userViewModel.Email };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }

                        //add user deets
                        RiftWorldEntities db = new RiftWorldEntities();
                        UserDetail newUserDeets = new UserDetail
                        {
                            UserId = user.Id,
                            DiscordName = userViewModel.DiscordName,
                            DiscordDiscriminator = userViewModel.DiscordDiscriminator,
                            ConsentFileName = null,
                            CurrentCharacterId = null,
                            IsApproved = false
                        };
                        db.UserDetails.Add(newUserDeets);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiftWorldEntities db = new RiftWorldEntities();
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            var userDeets = await db.UserDetails.Where(x => x.UserId == id).FirstAsync();
            ViewBag.CurrentCharacter = await db.Characters.Where(x => x.CharacterId == userDeets.CurrentCharacterId).Select(x => x.CharacterName).FirstOrDefaultAsync();
            ViewBag.PastCharacters = db.Characters.Where(x => x.PlayerId == id && x.IsRetired == true).Select(x => x.CharacterName).ToList();

            return View(new EditUserViewModel3()
            {
                Id = user.Id,
                Email = user.Email,
                DiscordName = userDeets.DiscordName,
                DiscordDiscriminator = userDeets.DiscordDiscriminator,
                ConsentFileName = userDeets.ConsentFileName,
                IsApproved = userDeets.IsApproved,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id, DiscordDiscriminator, DiscordName, CurrentCharacterId, ConsentFileName")] EditUserViewModel3 editUser,
            params string[] selectedRole)
        {
            RiftWorldEntities db = new RiftWorldEntities();
            var userDeets = await db.UserDetails.Where(x => x.UserId == editUser.Id).FirstAsync();
            ViewBag.CurrentCharacter = await db.Characters.Where(x => x.CharacterId == userDeets.CurrentCharacterId).Select(x => x.CharacterName).FirstOrDefaultAsync();
            ViewBag.PastCharacters = db.Characters.Where(x => x.PlayerId == editUser.Id && x.IsRetired == true).Select(x => x.CharacterName).ToList();

            //todo - finish this action
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = $"{editUser.DiscordName}#{editUser.DiscordDiscriminator:D4}";
                user.Email = editUser.Email;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                //added below 
                #region Updating User Details

                userDeets.DiscordDiscriminator = editUser.DiscordDiscriminator;
                userDeets.DiscordName = editUser.DiscordName;
                db.Entry(userDeets).State = EntityState.Modified;
                var result2 = await db.SaveChangesAsync();
                #endregion
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            editUser.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
            {
                Selected = selectedRole.Contains(x.Name),
                Text = x.Name,
                Value = x.Name
            });

            return View(editUser);
        }

        //Approve User
        [HttpGet]
        public ActionResult Approve(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiftWorldEntities db = new RiftWorldEntities();

            var user = db.AspNetUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var userRoles = userManager.GetRoles(id);
            var userDeets = db.UserDetails.Where(x => x.UserId == id).First();
            ViewBag.CurrentCharacter = db.Characters.Where(x => x.CharacterId == userDeets.CurrentCharacterId).Select(x => x.CharacterName).FirstOrDefault();
            ViewBag.PastCharacters = db.Characters.Where(x => x.PlayerId == id && x.IsRetired == true).Select(x => x.CharacterName).ToList();
            return View(new EditUserViewModel2()
            {
                Id = user.Id,
                Email = user.Email,
                DiscordName = userDeets.DiscordName,
                DiscordDiscriminator = userDeets.DiscordDiscriminator,
                ConsentFileName = userDeets.ConsentFileName,
                IsApproved = userDeets.IsApproved
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve([Bind(Include = "Email,Id, ConsentFileName, DiscordName, DiscordDiscriminator, IsApproved")] EditUserViewModel2 editUser, string submit, HttpPostedFileBase consentFile)
        {

            RiftWorldEntities db = new RiftWorldEntities();
            var userDeets = db.UserDetails.Where(x => x.UserId == editUser.Id).First();
            ViewBag.CurrentCharacter = db.Characters.Where(x => x.CharacterId == userDeets.CurrentCharacterId).Select(x => x.CharacterName).FirstOrDefault();
            ViewBag.PastCharacters = db.Characters.Where(x => x.PlayerId == editUser.Id && x.IsRetired == true).Select(x => x.CharacterName).ToList();

            if (ModelState.IsValid)
            {
                var user = db.AspNetUsers.Find(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                if (consentFile != null)
                {
                    string consentName = consentFile.FileName;

                    string ext = consentName.Substring(consentName.LastIndexOf('.'));
                    string[] goodExts = { ".pdf" };

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        consentName = editUser.DiscordName + "_" + editUser.DiscordDiscriminator + ext;
                        consentFile.SaveAs(Server.MapPath("~/Content/ConsentFiles/" + consentName));
                        UserDetail userDetail = db.UserDetails.Find(editUser.Id);
                        userDetail.ConsentFileName = consentName;
                        userDetail.IsApproved = true;
                        db.Entry(userDetail).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("", "That file type was the incorrect type. It needs to end with either .pdf ");
                        return View(new EditUserViewModel2()
                        {
                            Id = editUser.Id,
                            Email = editUser.Email,
                            DiscordName = editUser.DiscordName,
                            DiscordDiscriminator = editUser.DiscordDiscriminator,
                            ConsentFileName = editUser.ConsentFileName,
                            IsApproved = editUser.IsApproved
                        });
                    }
                }
                else if (editUser.ConsentFileName == null)
                {
                    ModelState.AddModelError("", "You need to add the consent form in order to approve a player Katherine. We went over this. You wanted it this way.");
                    return View(new EditUserViewModel2()
                    {
                        Id = editUser.Id,
                        Email = editUser.Email,
                        DiscordName = editUser.DiscordName,
                        DiscordDiscriminator = editUser.DiscordDiscriminator,
                        ConsentFileName = editUser.ConsentFileName,
                        IsApproved = editUser.IsApproved
                    });
                }

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View(new EditUserViewModel2()
            {
                Id = editUser.Id,
                Email = editUser.Email,
                DiscordName = editUser.DiscordName,
                DiscordDiscriminator = editUser.DiscordDiscriminator,
                ConsentFileName = editUser.ConsentFileName,
                IsApproved = editUser.IsApproved
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deny(string id)
        {
            RiftWorldEntities db = new RiftWorldEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.AspNetUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userDeets = db.UserDetails.Where(x => x.UserId == id).FirstOrDefault();
            if (userDeets == null)
            {
                //error
            }
            if (userDeets.IsApproved == true)
            {
                ViewBag.Message = "You can't deny someone who already is approved to be a user. If you're trying to remove them permanently, you must use the delete function. Also, how are you getting this error?";
                return View("Error");
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user2 = UserManager.FindById(id);

            UserManager.Delete(user2);

            db.UserDetails.Remove(userDeets);
            db.SaveChangesAsync();

            return RedirectToAction("Approvals", "Infos", null);
        }

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //[HttpGet]
        //public async Task<ActionResult> _Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var user = await UserManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(user);
        //}

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = UserManager.FindById(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                UserManager.Delete(user);

                RiftWorldEntities db = new RiftWorldEntities();
                var userDeets = db.UserDetails.Where(x => x.UserId == id).FirstOrDefault();
                var currentCharacter = userDeets.CurrentCharacterId;
                var consent = userDeets.ConsentFileName;

                string fullPath = Request.MapPath("~/Content/ConsentFiles/" + consent);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                db.UserDetails.Remove(userDeets);
                if (currentCharacter != null)
                {
                    var character = db.Characters.Where(c => c.CharacterId == currentCharacter).FirstOrDefault();
                    character.IsRetired = true;
                    db.Entry(character).State = EntityState.Modified;
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
