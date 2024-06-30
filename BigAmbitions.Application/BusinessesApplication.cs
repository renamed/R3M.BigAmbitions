using BigAmbitions.Application.Contracts;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using FluentValidation;

namespace BigAmbitions.Application;

public class BusinessesApplication : IBusinessesApplication
{
    private readonly IBusinessesRepository _businessRepository;
    private readonly IValidator<Business> _validator;

    public BusinessesApplication(IBusinessesRepository businessRepository, IValidator<Business> validator)
    {
        _businessRepository = businessRepository;
        _validator = validator;
    }

    public async Task<Business> AddAsync(Business business)
    {
        ValidateAndThrow(business);

        await _businessRepository.AddAsync(business);
        return business;
    }

    public ValueTask<Business?> FindAsync(int id)
    {
        return _businessRepository.FindAsync(id);
    }

    public async Task<Business> EditAsync(int id, Business business)
    {
        var existingBusiness = await FindAsync(id)
            ?? throw new Exception("Business not found");

        existingBusiness.Name = business.Name;
        existingBusiness.DailyRent = business.DailyRent;
        existingBusiness.Employees = business.Employees;

        ValidateAndThrow(existingBusiness);

        await _businessRepository.UpdateAsync(existingBusiness);
        return existingBusiness;
    }

    public async Task RemoveAsync(int id)
    {
        var existingBusiness = await FindAsync(id)
            ?? throw new Exception("Business not found");

        await _businessRepository.RemoveAsync(existingBusiness);
    }

    private void ValidateAndThrow(Business business)
    {
        var validationResult = _validator.Validate(business);
        if (!validationResult.IsValid)
        {
            throw new Exception(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
