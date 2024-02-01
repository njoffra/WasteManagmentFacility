using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WasteManagmentFacility.Data;
using WasteManagmentFacility.Models;

namespace WasteManagmentFacility.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, ILogger<AdminController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: AdminController
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        // GET: AdminController/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_userManager.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, FirstName,LastName,Email,PhoneNumber")] ApplicationUser scaffold)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = scaffold.FirstName,
                    LastName = scaffold.LastName,
                    Email = scaffold.Email,
                    PhoneNumber = scaffold.PhoneNumber,
                    UserName = scaffold.Email,
                };

                var result = await _userManager.CreateAsync(user);
                //if (result.Succeeded)
                //{
                //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //    var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
                //    var message = new Message(new string[] { user.Email }, "Aktivacija profila", $"Da aktivirate profil, <a href='{HtmlEncoder.Default.Encode(callback)}'>kliknite ovdje</a>.");
                //    await _emailSender.SendEmailAsync(message);
                //    return RedirectToAction(nameof(Index));
                //}
                //else
                //{
                //    foreach (var error in result.Errors)
                //    {
                //        ModelState.AddModelError("", error.Description);
                //    }
            }
            //}

            ViewData["Id"] = new SelectList(_userManager.Users, "Id", "Id", scaffold.Id);
            return RedirectToAction(nameof(Index));
            //return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, FirstName, LastName, Email, PhoneNumber")] ApplicationUser updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Email = updatedUser.Email;
                user.PhoneNumber = updatedUser.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(updatedUser);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpGet]
        public IActionResult CreateFacility()
        {
            var vehicles = _context.Vehicles.ToList();
            ViewBag.Vehicles = new SelectList(vehicles, "Id", "Model");
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateFacility(Facility model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("ModelState is valid.");
                    var vehicles = _context.Vehicles.ToList();
                    ViewBag.Vehicles = new SelectList(vehicles, "Id", "Model");
                    var newFacility = new Facility
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        Capacity = model.Capacity,
                        CurrentOccupancy = 0,
                        //Vehicles = new List<Vehicle>(),
                        // waste = new List<Waste>(),
                        Vehicles = model.Vehicles // Update to use VehicleIds
                    };

                    _logger.LogInformation("New facility created.");

                    _context.Facilities.Add(newFacility);
                    _context.SaveChanges();

                    _logger.LogInformation("SaveChanges completed.");

                    return RedirectToAction(nameof(Facility));
                }

                _logger.LogInformation("ModelState is not valid.");
                
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the facility.");
                throw; // Rethrow the exception for further analysis
            }
        }

        [HttpGet]
        public IActionResult Facility()
        {
            var facilities = _context.Facilities.Include(f => f.Vehicles).ToList();
            return View(facilities);

        }
        [HttpGet]
       
        public IActionResult EditFacility(Guid id)
        {
            var facility = _context.Facilities.Find(id);

            if (facility == null)
            {
                return NotFound();
            }

            ViewBag.Vehicles = new SelectList(_context.Vehicles, "Id", "Model");
            return View(facility);
        }

        [HttpPost]
       
        public IActionResult EditFacility(Guid id, Facility facilityModel)
        {
            try
            {
                if (id != facilityModel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var facility = _context.Facilities.Find(id);

                    if (facility == null)
                    {
                        return NotFound();
                    }

                    facility.Name = facilityModel.Name;

                    facility.Capacity = facilityModel.Capacity;
                    facility.Vehicles = facilityModel.Vehicles; // Update the VehicleIds

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Facility));
                }

                ViewBag.Vehicles = new SelectList(_context.Vehicles, "Id", "Model");
                return View(facilityModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing the facility.");
                throw; // Rethrow the exception for further analysis
            }
        }

        [HttpGet]
        public IActionResult DeleteFacility(Guid id)
        {
            var facility = _context.Facilities.Find(id);

            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        [HttpPost, ActionName("DeleteFacility")]
       
        public IActionResult DeleteConfirmedFacility(Guid id)
        {
            try
            {
                var facility = _context.Facilities.Find(id);

                if (facility == null)
                {
                    return NotFound();
                }

                _context.Facilities.Remove(facility);
                _context.SaveChanges();

                return RedirectToAction(nameof(Facility));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the facility.");
                throw; // Rethrow the exception for further analysis
            }
        }
        [HttpGet]
        public IActionResult DetailsFacility(Guid id)
        {
            var facility = _context.Facilities.Find(id);

            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        [HttpGet]
        public IActionResult CreateVehicle()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Vehicle()
        {
            var vehicles = _context.Vehicles.ToList(); // or _context.Facilities.ToList();
            return View(vehicles);

        }
        [HttpPost]
        public IActionResult CreateVehicle(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                var newVehicle = new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Model = vehicle.Model,
                    Plate = vehicle.Plate,
                    Capacity = vehicle.Capacity,
                    Available = vehicle.Available,
                    //FacilityId = vehicle.FacilityId
                };
                _context.Vehicles.Add(newVehicle);
                _context.SaveChanges();

                return RedirectToAction(nameof(Vehicle));
            }

            return View(vehicle);
        }

        [HttpGet]
        
        public IActionResult EditVehicle(Guid id)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost]
       
        public IActionResult EditVehicle(Guid id, Vehicle vehiclemodel)
        {
            try
            {
                if (id != vehiclemodel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var vehicle = _context.Vehicles.Find(id);

                    if (vehicle == null)
                    {
                        return NotFound();
                    }

                    vehicle.Model = vehiclemodel.Model;
                    vehicle.Plate = vehiclemodel.Plate;
                    vehicle.Capacity = vehiclemodel.Capacity;
                    vehicle.Available = vehiclemodel.Available;

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Vehicle));
                }

                return View(vehiclemodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing the vehicle.");
                throw; // Rethrow the exception for further analysis
            }
        }

        [HttpGet]
        public IActionResult DeleteVehicle(Guid id)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost, ActionName("DeleteVehicle")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmedVehicle(Guid id)
        {
            try
            {
                var vehicle = _context.Vehicles.Find(id);

                if (vehicle == null)
                {
                    return NotFound();
                }

                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();

                return RedirectToAction(nameof(Vehicle));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the vehicle.");
                throw; // Rethrow the exception for further analysis
            }
        }
        [HttpGet]
        public IActionResult DetailsVehicle(Guid id)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

    }
}
