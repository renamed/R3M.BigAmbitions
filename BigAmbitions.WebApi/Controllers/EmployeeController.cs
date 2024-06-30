using AutoMapper;
using BigAmbitions.Application.Contracts;
using BigAmbitions.Domain;
using BigAmbitions.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BigAmbitions.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeApplication _employeeApplication;
    private readonly IMapper _mapper;

    public EmployeeController(IEmployeeApplication employeeApplication, IMapper mapper)
    {
        _employeeApplication = employeeApplication;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeResponseDto>> AddAsync([FromBody] EmployeeRequestDto employeeRequestDto)
    {
        var employee = _mapper.Map<Employee>(employeeRequestDto);
        var addedEmployee = await _employeeApplication.AddAsync(employee);
        var employeeResponseDto = _mapper.Map<EmployeeResponseDto>(addedEmployee);
        return Created(Request.Path, employeeResponseDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeResponseDto>> FindAsync(int id)
    {
        var employee = await _employeeApplication.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        var employeeResponseDto = _mapper.Map<EmployeeResponseDto>(employee);
        return Ok(employeeResponseDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeResponseDto>> EditAsync(int id, [FromBody] EmployeeRequestDto employeeRequestDto)
    {
        var employee = _mapper.Map<Employee>(employeeRequestDto);
        var updatedEmployee = await _employeeApplication.EditAsync(id, employee);
        var employeeResponseDto = _mapper.Map<EmployeeResponseDto>(updatedEmployee);
        return Ok(employeeResponseDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        await _employeeApplication.RemoveAsync(id);
        return NoContent();
    }
}
