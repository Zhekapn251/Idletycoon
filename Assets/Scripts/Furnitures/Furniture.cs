using Datas.Furniture;
using DG.Tweening;
using Services;
using Services.Implementation;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Furnitures
{
    public abstract class Furniture : MonoBehaviour
    {
        public GameObject ModelContainer;
        public ParticleSystem VFX;
        public FurnitureData Data = new FurnitureData();
        public Transform Model;
        public string[] LevelPrefab;
        private IFurnitureService _furnitureService;
        protected IStaticDataService StaticDataService;
        private IPointerOverUIService _pointerOverUIService;


        public void Init(IFurnitureService furnitureService, IStaticDataService staticDataService, IPointerOverUIService pointerOverUIService)
        {
            StaticDataService = staticDataService;
            _furnitureService = furnitureService;
            _pointerOverUIService = pointerOverUIService;
        }

        private void OnMouseDown()
        {
            if (_pointerOverUIService.PointerIsOverUI) return;
            _furnitureService.PressOnFurniture(Data);
        }

        public void Upgrade()
        {
            if (Data.Level < LevelPrefab.Length)
            {
                DOTween.Sequence()
                    .AppendCallback(()=>VFX.Play())
                    .Append(Model.DOScale(Vector3.zero, 0.5f))
                    .AppendCallback(() =>
                    {
                        LoadModel();
                    })
                    .Append(Model.DOScale(Vector3.one, 0.5f));
                LoadModel();
            }
        }

        public void LoadModel()
        {
            if (Model != null)
            {
                Destroy(Model.gameObject);
            }
            
            GameObject model = ResourcesLoader.LoadResource<GameObject>(LevelPrefab[Data.Level - 1]);
            Model = Instantiate(model, ModelContainer.transform).transform;
            Model.localPosition = Vector3.zero;
        }
    }
}