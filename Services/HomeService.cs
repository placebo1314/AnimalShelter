using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Globalization;
using TestProject02.Daos;
using TestProject02.Intrfaces;
using TestProject02.Models;

namespace TestProject02.Services;

public class HomeService
{
    private readonly IAnimalsDao _animalsDao;
    const int pageSize = 5;

    public HomeService(IAnimalsDao animalsDao)
    {
        _animalsDao = animalsDao;
    }

    public AnimalsViewModel GetDisplayAnimalsData(string species, string sortBy, int page, string order, bool change)
    {
        AnimalsViewModel animals = new();

        animals.Species = GetSpecies(species);
        animals.Animals = (List<Animal>)_animalsDao.GetAll(species);

        int lastPage = (int)Math.Ceiling(animals.Animals.Count / (double)pageSize);
        animals.Page = page < 1 ? 1 : page;
        animals.EnableNext = page < lastPage ? true : false;
        animals.EnablePrevious = page > 1 ? true : false;

        animals.SortBy = sortBy ?? "Inclusion";
        animals.Order = GetOrder(change, order);

        animals.Animals = SortAnimals(animals.Animals, animals.Order, animals.SortBy).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return animals;
    }

    private string? GetSpecies(string species)
    {
        return species == "Mind" ? null : species;
    }
    private string GetOrder(bool change, string order)
    {
        if (change)
            return order == "asc" ? "desc" : "asc";
        return order ?? "asc";
    }

    private List<Animal> SortAnimals(List<Animal> animals, string order, string sortBy)
    {
        Func<Animal, object>? keySelector = null;
        switch (sortBy)
        {
            case "name":
                keySelector = x => x.Name;
                break;
            case "age":
                keySelector = x => x.Age;
                break;
            default:
                keySelector = x => x.Inclusion;
                break;
        }
        switch (order)
        {
            case "desc":
                animals = animals.OrderByDescending(keySelector).ToList();
                break;
            default:
                animals = animals.OrderBy(keySelector).ToList();
                break;
        }
        return animals;
    }

}
