using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface IAvailabilityService
{
    Task<ServiceResponse<IEnumerable<AvailabilityResponseDto>>>
        GetAvailableTablesByCustomerInputAsync(AvailabilityRequestDto availabilityRequestDto);
}