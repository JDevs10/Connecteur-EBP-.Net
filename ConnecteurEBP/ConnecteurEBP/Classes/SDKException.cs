using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConnecteurEBP.Classes
{
    [Serializable()]
    public class SDKException : Exception
    {
        #region Constructeurs
        /// <summary>
        /// Création d'une instance de SDKException
        /// </summary>
        public SDKException()
          : base()
        {
        }

        /// <summary>
        /// Création d'une instance de SDKException
        /// </summary>
        /// <param name="message">le message à retourner</param>
        public SDKException(string message)
          : base(message)
        {
        }

        /// <summary>
        /// Création d'une instance de SDKException
        /// </summary>
        /// <param name="message">le message à retourner</param>
        /// <param name="innerException">Exception enfant</param>
        public SDKException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        /// <summary>
        /// Création d'une instance de SDKException
        /// </summary>
        /// <param name="serializationInfo">Informations de serialisation</param>
        /// <param name="streamingContext">contexte</param>
        public SDKException(SerializationInfo serializationInfo, StreamingContext streamingContext)
          : base(serializationInfo, streamingContext)
        {
        }
        #endregion
    }
}
