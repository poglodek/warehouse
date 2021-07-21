using System.Collections.Generic;
using warehouse.Dto.Index;

namespace warehouse.Services.IRepositories
{
    public interface IIndexServices
    {
        List<IndexDto> GetIndexes();
    }
}