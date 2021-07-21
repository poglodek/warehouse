using System.Collections.Generic;
using warehouse.Dto.Index;

namespace warehouse.Services.IRepositories
{
    public interface IIndexServices
    {
        List<IndexDto> GetIndexes();
        IndexDto GetIndexById(int id);
        List<IndexDto> GetIndexByName(string name);
        int Create(IndexDto index);
        void Delete(int id);
    }
}