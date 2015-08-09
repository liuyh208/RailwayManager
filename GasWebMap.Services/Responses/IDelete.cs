using GasWebMap.Services.Responses;

namespace GasWebMap.Services
{
    internal interface IDelete<T> where T : DeleteOperation
    {
        ResponseResult Delete(T ids);
    }
}