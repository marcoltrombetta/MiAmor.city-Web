using AutoMapper;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Transactions;
using MiAmor.Data;

namespace MiAmor.Web.Areas.Admin.Controllers
{
    public class AdminFileUploadController : Controller
    {
        private IPictureService _PictureService;
        public AdminFileUploadController(IPictureService PictureService)
            {
                _PictureService = PictureService;
            }

        // GET: Admin/FileUpload
        [HttpPost]
        public async Task<JsonResult> SaveFiles(PictureUpdateTableData PictureUpdateTableData, string FileType)
        {
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;
                bool b = await Task.Run(() => SaveFile(file, fileName));
                PictureUpdateTableData.url = "~/App_Data/Tmp/FileUpload/" + fileName;
                FileType = "Picture";
            }
            bool ReturnData = true;
            switch (FileType)
            {
                case "Picture":
                    ReturnData = SetPictureUtility(PictureUpdateTableData);
                    break;
            }
            return new JsonResult { Data = new { Message = Message, Status = flag } };
        }

        #region utilities
        private bool SetPictureUtility(PictureUpdateTableData PictureUpdateTableData)
        {
            bool b=_PictureService.InsertPictureAndRelevantTable(PictureUpdateTableData);
            return b;
        }
        private bool SaveFile(HttpPostedFileBase File, string FileName)
        {

            try
            {
                File.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/App_Data/Tmp/FileUpload"), FileName));
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}