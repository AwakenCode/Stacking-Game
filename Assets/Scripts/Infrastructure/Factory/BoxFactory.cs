using GameplayEntities.Box;
using Service.UnityContext;
using StaticData;

namespace Infrastructure.Factory
{
    public class BoxFactory : IObjectFactory<Box>
    {
        private readonly FactoryData _data;
        private readonly UnityInstantiater _unityInstantiater;

        public BoxFactory(FactoryData data, UnityInstantiater unityInstantiater)
        {
            _data = data;
            _unityInstantiater = unityInstantiater;
        }

        public Box CreateBox()
        {
            return _unityInstantiater.Instantiate(_data.BoxTemplate);
        }
    }
}