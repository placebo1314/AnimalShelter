using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Linq;
using TestProject02.Daos;
using TestProject02.Intrfaces;
using TestProject02.Models;
using static System.Net.WebRequestMethods;


namespace TestProject02.Services;

public class AnimalService
{
    private readonly IAnimalsDao _animalsDao;
    private readonly IAccountDao _userDao;

    public AnimalService(IAnimalsDao animalsDao, IAccountDao userDao)
    {
        _animalsDao = animalsDao;
        _userDao = userDao;
    }

    public string AddAnimal(Models.Animal animal, IFormFile image, IFormFile bgImage, string path, string folder)
    {
        List<IFormFile> postedFiles = new List<IFormFile>();
        postedFiles.Add(image);
        postedFiles.Add(bgImage);
        string message = "";
        FileValidationModel validate = ValidateFiles(postedFiles);
        if (validate.Valid)
            postedFiles = validate.List;
        else
            return validate.Message;
        
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        foreach (IFormFile postedFile in postedFiles)
        {
            string fileName = getUniqueFileName(1, String.Format("{0}\\{1}",path, postedFile.FileName), Path.GetFileName(postedFile.FileName));
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);

                if(postedFile.Name=="image")
                    animal.Image = String.Format("/{0}/{1}", folder, fileName);
                else
                    animal.BgImage = String.Format("/{0}/{1}", folder, fileName);
            }
        }
        _animalsDao.Add(animal);
        return "Added: " + animal.Name;
    }

    public IEnumerable<Animal> GetAllAnimals(string? filter)
    {
        return _animalsDao.GetAll(filter);
    }

    internal string DeleteAnimal(int id, string path)
    {
        Animal animal = _animalsDao.Get(id);
        if(animal.Image != null)
            System.IO.File.Delete(path + "\\" + animal.Image);
        if (animal.BgImage != null)
            System.IO.File.Delete(path + "\\" + animal.BgImage);
        _animalsDao.Delete(id);
        return animal.Name + " deleted!";
    }

    internal string EditAnimal(Animal animal, IFormFile image, IFormFile bgImage, string path, string folder)
    {
        List<IFormFile> postedFiles = new List<IFormFile>();
        postedFiles.Add(image);
        postedFiles.Add(bgImage);
        string message = "";
        FileValidationModel validate = ValidateFiles(postedFiles);
        if (validate.Valid)
            postedFiles = validate.List;
        else
            return validate.Message;

        Animal original = _animalsDao.Get(animal.Id);

        foreach (IFormFile postedFile in postedFiles)
        {
            string fileName;
            if (postedFile.Name == "image")
                if (original.Image == null)
                {
                    fileName = getUniqueFileName(1, String.Format("{0}\\{1}", path, postedFile.FileName), Path.GetFileName(postedFile.FileName));
                    animal.Image = String.Format("/{0}/{1}", folder, fileName);
                }
                else
                    fileName = String.Join("", original.Image.Skip(original.Image.LastIndexOf("/") + 1).Take(original.Image.Length - 1));
            else
                if (original.BgImage == null)
                {
                    fileName = getUniqueFileName(1, String.Format("{0}\\{1}", path, postedFile.FileName), Path.GetFileName(postedFile.FileName));
                    animal.BgImage = String.Format("/{0}/{1}", folder, fileName);
                }
                else
                    fileName = String.Join("", original.BgImage.Skip(original.BgImage.LastIndexOf("/") + 1).Take(original.BgImage.Length - 1));
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);
                message += string.Format("{0} uploaded.", fileName);
            }
        }
        if (animal.Image == null)
            animal.Image = original.Image;
        if (animal.BgImage == null)
            animal.BgImage = original.BgImage;
        _animalsDao.Edit(animal);
        return animal.Name + " Edited." + message;
    }

    internal Animal GetAnimal(int id)
    {
        return _animalsDao.Get(id);
    }

    private string getUniqueFileName(int i, string fullpath, string filename)
    {
        string lstDir = fullpath.Substring(0, fullpath.LastIndexOf("\\"));

        string name = Path.GetFileName(fullpath);
        string path = fullpath;

        if (name != filename)
            path = Path.Combine(lstDir, filename);
        if (System.IO.File.Exists(path))
        {
            string ext = Path.GetExtension(name);
            name = Path.GetFileNameWithoutExtension(name);
            i++;
            filename = getUniqueFileName(i, fullpath, name + "_" + i + ext);
        }

        return filename;
    }

    private FileValidationModel ValidateFiles(List<IFormFile> files)
    {
        FileValidationModel result = new FileValidationModel();
        result.List = new List<IFormFile>();
        foreach (IFormFile file in files)
            if (file != null)
                if (ValidateExtension(file))
                    result.List.Add(file);
                else
                {
                    result.Message = "Cant upload the file. (Only .png or .jpg and under 2Mb)";
                    result.Valid = false;
                    return result;
                }
        result.Valid = true;
        return result;
    }

    private bool ValidateExtension(IFormFile file)
    {
        string[] extensions = new string[] { ".png", ".jpg", ".jpeg" };
        var extension = Path.GetExtension(file.FileName);
        return extensions.Contains(extension.ToLower());
    }


}
