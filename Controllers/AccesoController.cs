using Microsoft.AspNetCore.Mvc;
using ArrendadoraM.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ArrendadoraM.Controllers
{
    public class AccesoController : Controller
    {

        private readonly IConfiguration _conf;

        public AccesoController(IConfiguration conf)
        {
            this._conf = conf;
        }

        public async Task<IActionResult> Index()
        {
            Usuarios modelo = new Usuarios();
            string Usuario, Contrasena;
            string IdRol;
            if(HttpMethods.IsPost(Request.Method)){
                Usuario = Convert.ToString(Request.Form["Usuario"]);
                Contrasena = Convert.ToString(Request.Form["Contrasena"]);

                using (MySqlConnection cnx = new MySqlConnection(_conf.GetConnectionString("DevConnection"))){
                cnx.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.Connection = cnx;
                comm.CommandText = "SELECT * FROM personal WHERE Usuario ='"+Usuario+"' AND Contrasena='"+Contrasena+"'";
                var dr = comm.ExecuteReader();
                if (dr.Read()){
                    cnx.Close();
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, Usuario),
                        new Claim("Usuario", Usuario)
                    };
                    cnx.Open();
                    MySqlCommand com = new MySqlCommand();
                    com.Connection = cnx;
                    com.CommandText = "SELECT idRol FROM personal WHERE Usuario ='"+Usuario+"' AND Contrasena='"+Contrasena+"'";
                    IdRol = Convert.ToString(com.ExecuteScalar());
                    claims.Add(new Claim(ClaimTypes.Role, IdRol));
                    cnx.Close();
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("IndexLogin", "Arrendadora");
                } else {
                    cnx.Close();
                    return RedirectToAction("Error", "Arrendadora");
                }
                }
            }
            return View();
        }

        
        public async Task<IActionResult> Salir(){
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Acceso");
        }
    }
}
