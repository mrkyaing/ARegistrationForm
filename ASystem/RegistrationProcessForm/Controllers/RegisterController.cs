using Microsoft.AspNetCore.Mvc;
using RegistrationProcessForm.Models;

namespace RegistrationProcessForm.Controllers {
    public class RegisterController : Controller {
        public static IList<RegisterModel> registerModels = new List<RegisterModel>();
        public IActionResult Register() {
            return View(new RegisterModel() { EmployeeID = "", Name = "" });
        }
        [HttpPost]
        public IActionResult Register(RegisterModel registerModel) {
            bool isValid = true;
            if (string.IsNullOrEmpty(registerModel.EmployeeID)) {
                isValid = false;
                ModelState.AddModelError("EmployeeID", "Please Enter the Employee ID");
            }
            if (string.IsNullOrEmpty(registerModel.Name)) {
                isValid = false;
                ModelState.AddModelError("Name", "Please Enter the Employee Name");
            }
            if (CheckDataIsAlreadyExist(registerModel)) {
                isValid = false;
                ModelState.AddModelError("EmployeeID", "Employee ID already exists");
            }
            if (isValid) {
                registerModels.Add(registerModel);
                return RedirectToAction("Index", registerModel);
            }
            return View(registerModel);
        }
        public IActionResult Index() {
            return View(registerModels);
        }
        private bool CheckDataIsAlreadyExist(RegisterModel registerModel) {
            return registerModels.Any(x => x.EmployeeID == registerModel.EmployeeID);
        }

        [HttpDelete]
        public IActionResult Delete(string id) {
            var registerModel = registerModels.FirstOrDefault(x => x.EmployeeID == id);
            if (registerModel != null) {
                registerModels.Remove(registerModel);
                return Json(new { success = true, Message = "Employe deleted successfully!" });
            }
            return Json(new { success = false, Message = "Employe not found!" });
        }

        public IActionResult Edit(string id) {
            var registerModel = registerModels.FirstOrDefault(x => x.EmployeeID == id);
            if (registerModel != null) {
                return View("Edit", registerModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(RegisterModel registerModel) {
            if (string.IsNullOrEmpty(registerModel.Name)) {
                ModelState.AddModelError("Name", "Please Enter the Employee Name");
                return View("Edit", registerModel);
            }
            var existingRegisterModel = registerModels.FirstOrDefault(x => x.EmployeeID == registerModel.EmployeeID);
            if (existingRegisterModel != null) {
                existingRegisterModel.Name = registerModel.Name;
                existingRegisterModel.Phone = registerModel.Phone;
            }
            return RedirectToAction("Index");
        }
    }
}
