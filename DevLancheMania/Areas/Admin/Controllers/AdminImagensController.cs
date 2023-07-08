using DevLancheMania.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevLancheMania.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _configurationImagens;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminImagensController( IOptions<ConfigurationImagens> configurationImagens, 
            IWebHostEnvironment hostEnvironment)
        {
            _configurationImagens = configurationImagens.Value;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if(files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error : Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if(files.Count > 10)
            {
                ViewData["Erro"] = "Error : Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            long size = files.Sum(f=> f.Length);

            var filePathsName = new List<string>();

            var filePath = Path.Combine(_hostEnvironment.WebRootPath,_configurationImagens.NomePastaImagensProdutos);

            foreach(var formFile in files)
            {
                if(formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif")
                    || formFile.FileName.Contains(".png"))
                {
                    var fileNamePath = string.Concat(filePath, "\\", formFile.FileName);

                    filePathsName.Add(fileNamePath);

                    using(var stream = new FileStream(fileNamePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +
                $"com tamanho total de : {size} bytes";

            ViewBag.Arquivos = filePathsName;

            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagespath = Path.Combine(_hostEnvironment.WebRootPath,
                _configurationImagens.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagespath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesProduto = _configurationImagens.NomePastaImagensProdutos;

            if(files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagespath}";
            }

            model.Files = files;

            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_hostEnvironment.WebRootPath,
                _configurationImagens.NomePastaImagensProdutos + "\\", fname);

            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                System.IO.File.Delete(_imagemDeleta);

                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
            }

            return View("index");
        }
    }
}
