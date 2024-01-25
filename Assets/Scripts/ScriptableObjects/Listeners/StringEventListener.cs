using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
public class StringEventListener : MonoBehaviour
{
	[SerializeField] private StringEvent _event = default;
 
	public UnityEvent<string> listener;
	private void OnEnable()
	{
		_event?.Subscribe(Respond);
	}
 
	private void OnDisable()
	{
		_event?.Unsubscribe(Respond);
	}
 
	private void Respond(string value)
	{
		listener?.Invoke(value);
	}
}