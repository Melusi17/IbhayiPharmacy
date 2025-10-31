using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Data;
using Microsoft.AspNetCore.Authorization;
using IbhayiPharmacy.Models.PharmacistVM;

namespace IbhayiPharmacy.Areas.Identity.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ProfileModel> _logger;

        public ProfileModel(
            UserManager<IdentityUser> userManager,
            ApplicationDbContext db,
            ILogger<ProfileModel> logger)
        {
            _userManager = userManager;
            _db = db;
            _logger = logger;
        }

        [BindProperty]
        public ProfileVM Profile { get; set; }

        public List<string> CurrentAllergies { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadProfileDataAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdatePersonalInfoAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadProfileDataAsync(user);
                return Page();
            }

            try
            {
                // Update cellphone number
                var applicationUser = await _db.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                if (applicationUser != null)
                {
                    applicationUser.CellphoneNumber = Profile.CellphoneNumber;
                    _db.ApplicationUsers.Update(applicationUser);
                    await _db.SaveChangesAsync();

                    _logger.LogInformation("User {UserId} updated their cellphone number", user.Id);
                    TempData["SuccessMessage"] = "Your personal information has been updated successfully!";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating personal information for user {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating your information. Please try again.");
            }

            await LoadProfileDataAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadProfileDataAsync(user);
                return Page();
            }

            try
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(
                    user,
                    Profile.CurrentPassword,
                    Profile.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    _logger.LogInformation("User {UserId} changed their password successfully.", user.Id);
                    TempData["SuccessMessage"] = "Your password has been changed successfully!";

                    // Clear the password fields
                    Profile.CurrentPassword = string.Empty;
                    Profile.NewPassword = string.Empty;
                    Profile.ConfirmNewPassword = string.Empty;
                }
                else
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "An error occurred while changing your password. Please try again.");
            }

            await LoadProfileDataAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAllergiesAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            try
            {
                // Get customer record (it should exist from registration)
                var customer = await _db.Customers
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

                if (customer != null)
                {
                    // Remove existing allergies
                    var existingAllergies = _db.Custormer_Allergies
                        .Where(ca => ca.CustomerID == customer.CustormerID);
                    _db.Custormer_Allergies.RemoveRange(existingAllergies);

                    // Add new selected allergies
                    if (Profile.SelectedAllergyIds != null && Profile.SelectedAllergyIds.Any())
                    {
                        foreach (var allergyId in Profile.SelectedAllergyIds)
                        {
                            var customerAllergy = new Custormer_Allergy
                            {
                                CustomerID = customer.CustormerID,
                                Active_IngredientID = allergyId
                                // Don't use DateRecorded if it doesn't exist
                            };
                            _db.Custormer_Allergies.Add(customerAllergy);
                        }
                    }

                    await _db.SaveChangesAsync();
                    _logger.LogInformation("User {UserId} updated their allergies. Count: {AllergyCount}",
                        user.Id, Profile.SelectedAllergyIds?.Count ?? 0);

                    TempData["SuccessMessage"] = "Your allergy information has been updated successfully!";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Customer record not found. Please contact support.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating allergies for user {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating your allergy information. Please try again.");
            }

            await LoadProfileDataAsync(user);
            return Page();
        }

        private async Task LoadProfileDataAsync(IdentityUser user)
        {
            var applicationUser = await _db.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            var customer = await _db.Customers
                .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

            if (applicationUser != null)
            {
                Profile = new ProfileVM
                {
                    Name = applicationUser.Name,
                    Surname = applicationUser.Surname,
                    Email = applicationUser.Email,
                    CellphoneNumber = applicationUser.CellphoneNumber,
                    IDNumber = applicationUser.IDNumber,
                    CustomerSince = "Recently", // Simple text instead of RegistrationDate
                    AllergyList = await GetAllergyListAsync()
                };

                // Load current allergies
                if (customer != null)
                {
                    var currentAllergies = await _db.Custormer_Allergies
                        .Include(ca => ca.Active_Ingredient)
                        .Where(ca => ca.CustomerID == customer.CustormerID)
                        .Select(ca => ca.Active_Ingredient.Name)
                        .ToListAsync();

                    CurrentAllergies = currentAllergies;

                    // Set selected allergy IDs
                    var selectedAllergyIds = await _db.Custormer_Allergies
                        .Where(ca => ca.CustomerID == customer.CustormerID)
                        .Select(ca => ca.Active_IngredientID)
                        .ToListAsync();

                    Profile.SelectedAllergyIds = selectedAllergyIds;
                }
            }
        }

        private async Task<List<SelectListItem>> GetAllergyListAsync()
        {
            // Use all active ingredients without IsActive filter
            return await _db.Active_Ingredients
                .OrderBy(a => a.Name)
                .Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Active_IngredientID.ToString()
                })
                .ToListAsync();
        }
    }
}