using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace InternetMagazine.Util
{
    class LoadLogic
    {
        public static string UploadLogic(HttpPostedFileBase file,HttpServerUtilityBase Server)
        {
            if (file != null)
            {
                if (file.ContentType == "image/jpeg")
                {
                    // получаем имя файла
                    string filename = DateTime.Now.ToString("ddMMMMyyyyHHmmss") + ".jpg";
                    string path = "/Images/tmp/" + filename;

                    // сохраняем файл в папку Files в проекте
                    file.SaveAs(Server.MapPath(path));

                    return path;
                }

            }
            throw new Exception("Изображение не загружено");
        }
      
    }
}
