using System.Collections.Generic;
using warehouse.Dto.ShippingInfo;

namespace warehouse.Services.IRepositories
{
    public interface IShippingInfoServices
    {
        List<ShippingInfoDto> GetShippingInfoDtoList();

        ShippingInfoDto GetShippingInfoDtoById(int id);

        List<ShippingInfoDto> GetShippingInfoDtoByTarget(string target);

        List<ShippingInfoDto> GetShippingInfoDtoByTrackNumber(string trackNumber);

        List<ShippingInfoDto> GetShippingInfoDtoByPriority(string priority);

        List<ShippingInfoDto> GetShippingInfoDtoByClientId(int id);

        int CreateShippingInfo(ShippingInfoCreateDto shippingInfoCreateDto);

        void UpdateShippingInfo(ShippingInfoUpdateDto shippingInfoUpdateDto, int id);

        void DeleteShippingInfoById(int id);
    }
}