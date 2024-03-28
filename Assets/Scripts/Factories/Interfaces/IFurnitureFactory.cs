using Cysharp.Threading.Tasks;
using Datas.Furniture;
using Furnitures;

namespace Factories.Interfaces
{
    public interface IFurnitureFactory
    {
        UniTask<Furniture> Create(FurnitureType furnitureType);
    }
}