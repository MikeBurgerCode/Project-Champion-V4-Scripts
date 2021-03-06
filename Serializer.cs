﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class Serializer  {



	public TData DeserializeFromString<TData>(string settings)
	{
		byte[] b = Convert.FromBase64String(settings);
		using (var stream = new MemoryStream(b))
		{
			var formatter = new BinaryFormatter();
			stream.Seek(0, SeekOrigin.Begin);
			return (TData)formatter.Deserialize(stream);
		}
	}

	public  string SerializeToString<TData>(TData settings)
	{
		using (var stream = new MemoryStream())
		{
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, settings);
			stream.Flush();
			stream.Position = 0;
			return Convert.ToBase64String(stream.ToArray());
		}
	}
}
