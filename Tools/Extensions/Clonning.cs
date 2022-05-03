//

//

using JovDK.SerializingTools.Json;

namespace JovDK.SerializingTools
{

    public static class SerializingTools
    {

        public static T DeepClone<T>(this T _object)
        {

            return _object.SerializeObjectToJSON().DeserializeJsonToObject<T>();

        }

    }

}
