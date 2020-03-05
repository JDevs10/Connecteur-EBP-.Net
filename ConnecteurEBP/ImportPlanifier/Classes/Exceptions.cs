using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ImportPlanifier.Classes
{
    [Serializable()]
    class Exceptions : Exception
    {
        #region Constructeurs
    /// <summary>
    /// Création d'une instance de SDKException
    /// </summary>
    public Exceptions()
      : base()
    {
    }

    /// <summary>
    /// Création d'une instance de SDKException
    /// </summary>
    /// <param name="message">le message à retourner</param>
    public Exceptions(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Création d'une instance de SDKException
    /// </summary>
    /// <param name="message">le message à retourner</param>
    /// <param name="innerException">Exception enfant</param>
    public Exceptions(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Création d'une instance de SDKException
    /// </summary>
    /// <param name="serializationInfo">Informations de serialisation</param>
    /// <param name="streamingContext">contexte</param>
    public Exceptions(SerializationInfo serializationInfo, StreamingContext streamingContext)
      : base(serializationInfo, streamingContext)
    {
    }
    #endregion
    }
}
