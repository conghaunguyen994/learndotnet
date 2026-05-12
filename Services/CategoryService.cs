using learndotnet.Models;
using learndotnet.Repositories;

namespace learndotnet.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _categoryRepository.GetAllCategories();
    }

    public Category? GetCategoryById(int id)
    {
        return _categoryRepository.GetCategoryById(id);
    }

    public void AddCategory(Category category)
    {
        _categoryRepository.AddCategory(category);
    }

    public void UpdateCategory(Category category)
    {
        _categoryRepository.UpdateCategory(category);
    }

    public void DeleteCategory(int id)
    {
        _categoryRepository.DeleteCategory(id);
    }
}