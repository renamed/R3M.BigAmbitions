using AutoMapper;
using BigAmbitions.Application.Contracts;
using BigAmbitions.Domain;
using BigAmbitions.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigAmbitions.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessController : ControllerBase
{
    private readonly IBusinessesApplication _businessApplication;
    private readonly IMapper _mapper;

    public BusinessController(IBusinessesApplication businessApplication, IMapper mapper)
    {
        _businessApplication = businessApplication;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<BusinessResponseDto>> AddAsync([FromBody] BusinessRequestDto businessRequestDto)
    {
        var business = _mapper.Map<Business>(businessRequestDto);
        var addedBusiness = await _businessApplication.AddAsync(business);
        var businessResponseDto = _mapper.Map<BusinessResponseDto>(addedBusiness);
        return Created(Request.Path, businessResponseDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessResponseDto>> FindAsync(int id)
    {
        var business = await _businessApplication.FindAsync(id);
        if (business == null)
        {
            return NotFound();
        }
        var businessResponseDto = _mapper.Map<BusinessResponseDto>(business);
        return Ok(businessResponseDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BusinessResponseDto>> EditAsync(int id, [FromBody] BusinessRequestDto businessRequestDto)
    {
        var business = _mapper.Map<Business>(businessRequestDto);
        var updatedBusiness = await _businessApplication.EditAsync(id, business);
        var businessResponseDto = _mapper.Map<BusinessResponseDto>(updatedBusiness);
        return Ok(businessResponseDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        await _businessApplication.RemoveAsync(id);
        return NoContent();
    }
}
