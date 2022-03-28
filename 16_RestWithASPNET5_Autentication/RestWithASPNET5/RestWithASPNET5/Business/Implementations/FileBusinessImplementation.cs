using Microsoft.AspNetCore.Http;
using RestWithASPNET5.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;
            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            //Cria um objeto que vai ter as informações que ele vai retornar: Nome do arquivo, tipo e url pra fazer download
            FileDetailVO fileDetail = new FileDetailVO();
            //dscobrindo a extensão do arquivo
            var fileType = Path.GetExtension(file.FileName);
            //montando a base url baseado no host
            var baseUrl = _context.HttpContext.Request.Host;
            //verificando se é alguma das extensões abaixo
            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
            {
                //armazenando o nome do arquivo numa variável
                var docName = Path.GetFileName(file.FileName);
                // se file for diferente de nulo e maior que 0 procede com a gravação
                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName);

                    //abrindo um stream com o sistema de arquivos da máquina
                    using var stream = new FileStream(destination, FileMode.Create);
                    //gravando no disco
                    await file.CopyToAsync(stream);
                }
            }
            return fileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>();
            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }
            return list;
        }
    }
}
