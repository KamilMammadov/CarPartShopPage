using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Extension;
using TechStore.Models;

namespace TechStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            Dictionary<string, string> setting = await _context.Settings.ToDictionaryAsync(k => k.Key, v => v.Value);

            return View(setting);
        }

        public async Task<IActionResult> Update(string key)
        {
            if (key == null) return BadRequest();
            Setting setting = await _context.Settings.FirstOrDefaultAsync(k => k.Key == key);
            if (setting == null) return NotFound();
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string key, Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View(setting);
            }

            if (key == null) return BadRequest();
            if (key != setting.Key) return NotFound();

            Setting dbSetting = await _context.Settings.FirstOrDefaultAsync(k => k.Key == key);

            if (dbSetting == null) return NotFound();

            if (setting.Key== "paypal")
            {
                if (setting != null)
                {
                    if (setting.Paypal == null)
                    {
                        ModelState.AddModelError("Paypal", "Sekil Mutleq Olmalidi");
                        return View(dbSetting);
                    }
                    if (setting.Paypal.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Paypal", " type should be png");
                        return View();
                    }
                    if (setting.Paypal.Length > 20000)
                    {
                        ModelState.AddModelError("Paypal", " length should be less than 20k");
                        return View();
                    }

                    Helper.DeleteFile(_env, dbSetting.Value, "assets", "images");
                    dbSetting.Value = setting.Paypal.CreateFile(_env, "assets", "images");
                }

            }
            else if (setting.Key == "discover")
            {
                if (setting != null)
                {
                    if (setting.Discover == null)
                    {
                        ModelState.AddModelError("Discover", "Sekil Mutleq Olmalidi");
                        return View(dbSetting);
                    }
                    if (setting.Discover.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Discover", " type should be png");
                        return View();
                    }
                    if (setting.Discover.Length > 20000)
                    {
                        ModelState.AddModelError("Discover", " length should be less than 20k");
                        return View();
                    }

                    Helper.DeleteFile(_env, dbSetting.Value, "assets", "images" );
                    dbSetting.Value = setting.Discover.CreateFile(_env, "assets", "images" );
                }
            }
            else if (setting.Key == "visa")
            {
                if (setting != null)
                {
                    if (setting.Visa == null)
                    {
                        ModelState.AddModelError("Visa", "Sekil Mutleq Olmalidi");
                        return View(dbSetting);
                    }
                    if (setting.Visa.ContentType != "image/png")
                    {
                        ModelState.AddModelError("Visa", " type should be png");
                        return View();
                    }
                    if (setting.Visa.Length > 20000)
                    {
                        ModelState.AddModelError("Visa", " length should be less than 20k");
                        return View();
                    }

                    Helper.DeleteFile(_env, dbSetting.Value, "assets", "images" );
                    dbSetting.Value = setting.Visa.CreateFile(_env, "assets", "images" );
                }
            }
            else if (setting.Key == "mastercard")
            {
                if (setting != null)
                {
                    if (setting.MasterCard == null)
                    {
                        ModelState.AddModelError("MasterCard", "Sekil Mutleq Olmalidi");
                        return View(dbSetting);
                    }
                    if (setting.MasterCard.ContentType != "image/png")
                    {
                        ModelState.AddModelError("MasterCard", " type should be png");
                        return View();
                    }
                    if (setting.MasterCard.Length > 20000)
                    {
                        ModelState.AddModelError("MasterCard", " length should be less than 20k");
                        return View();
                    }

                    Helper.DeleteFile(_env, dbSetting.Value, "assets", "images" );
                    dbSetting.Value = setting.MasterCard.CreateFile(_env, "assets", "images" );
                }
            }
            else if (setting.Key == "americanexpress")
            {
                if (setting != null)
                {
                    if (setting.AmericanExpress == null)
                    {
                        ModelState.AddModelError("AmericanExpress", "Sekil Mutleq Olmalidi");
                        return View(dbSetting);
                    }
                    if (setting.AmericanExpress.ContentType != "image/png")
                    {
                        ModelState.AddModelError("AmericanExpress", " type should be png");
                        return View();
                    }
                    if (setting.AmericanExpress.Length > 20000)
                    {
                        ModelState.AddModelError("AmericanExpress", " length should be less than 20k");
                        return View();
                    }

                    Helper.DeleteFile(_env, dbSetting.Value, "assets", "images" );
                    dbSetting.Value = setting.AmericanExpress.CreateFile(_env, "assets", "images" );
                }
            }
            else if (setting.Key == "footerLogo")
            {
                if (setting != null)
                {
                    if (setting.FooterImg == null)
                    {
                        ModelState.AddModelError("FooterImg", "Sekil Mutleq Olmalidi");
                        return View(dbSetting);
                    }
                    if (setting.FooterImg.ContentType != "image/png")
                    {
                        ModelState.AddModelError("FooterImg", " type should be png");
                        return View();
                    }
                    if (setting.FooterImg.Length > 20000)
                    {
                        ModelState.AddModelError("FooterImg", " length should be less than 20k");
                        return View();
                    }

                    Helper.DeleteFile(_env, dbSetting.Value, "assets", "images" );
                    dbSetting.Value = setting.FooterImg.CreateFile(_env, "assets", "images" );
                }
            }
            else if (setting.Key == "callLogo")
            {
                if (setting != null)
                {
                    if (setting.LogoImg == null)
                    {
                        ModelState.AddModelError("LogoImg", "Sekil Mutleq Olmalidi");
                        return View(dbSetting);
                    }
                    if (setting.LogoImg.ContentType != "image/png")
                    {
                        ModelState.AddModelError("LogoImg", " type should be png");
                        return View();
                    }
                    if (setting.LogoImg.Length > 20000)
                    {
                        ModelState.AddModelError("LogoImg", " length should be less than 20k");
                        return View();
                    }

                    Helper.DeleteFile(_env, dbSetting.Value, "assets", "images" );
                    dbSetting.Value = setting.LogoImg.CreateFile(_env, "assets", "images" );
                }
            }
            else
            {
                dbSetting.Value = setting.Value;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
