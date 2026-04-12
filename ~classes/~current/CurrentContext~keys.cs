using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;

namespace Ans.Net10.Web
{

	/// <summary>
	/// Операции с ключами HttpContext.Items
	/// </summary>
	public partial class CurrentContext
	{

		/* methods */


		/// <summary>
		/// Устанавливает для ключа значение 'on'
		/// </summary>
		public void SetKeyOn(
			string key)
		{
			HttpContext.Items.Add(
				_getValueKeyOn(key), "on");
		}


		/// <summary>
		/// Устанавливает для ключа значение 'off'
		/// </summary>
		public void SetKeyOff(
			string key)
		{
			HttpContext.Items.Add(
				_getValueKeyOff(key), "off");
		}


		/// <summary>
		/// Устанавливает для ключа значение IHtmlContent
		/// </summary>
		public void SetKey(
			string key,
			Func<dynamic, IHtmlContent> value)
		{
			HttpContext.Items[key] = HttpContext.GetStringFromRazor(value);
		}


		/// <summary>
		/// Устанавливает для ключа значение string
		/// </summary>
		public void SetKey(
			string key,
			string value)
		{
			HttpContext.Items[key] = value;
		}


		/// <summary>
		/// Устанавливает для ключа значение int
		/// </summary>
		public void SetKey(
			string key,
			int value)
		{
			HttpContext.Items[key] = value;
		}


		/// <summary>
		/// Устанавливает для ключа значение bool
		/// </summary>
		public void SetKey(
			string key,
			bool value)
		{
			HttpContext.Items[key] = value;
		}


		/// <summary>
		/// Устанавливает для ключа значение DateTime
		/// </summary>
		public void SetKey(
			string key,
			DateTime value)
		{
			HttpContext.Items[key] = value;
		}


		/// <summary>
		/// Устанавливает для ключа значение DateOnly
		/// </summary>
		public void SetKey(
			string key,
			DateOnly value)
		{
			HttpContext.Items[key] = value;
		}


		/// <summary>
		/// Устанавливает для ключа значение TimeOnly
		/// </summary>
		public void SetKey(
			string key,
			TimeOnly value)
		{
			HttpContext.Items[key] = value;
		}


		/* functions */


		/// <summary>
		/// Проверяет установлен ли ключ в 'on'
		/// </summary>
		public bool IsKeyOn(
			string key)
		{
			return HttpContext.Items.ContainsKey(
				_getValueKeyOn(key));
		}


		/// <summary>
		/// Проверяет не установлен ли ключ в 'off'
		/// </summary>
		public bool IsKeyNotOff(
			string key)
		{
			return !HttpContext.Items.ContainsKey(
				_getValueKeyOff(key));
		}


		/// <summary>
		/// Проверяет, что установлен хотя бы один ключ
		/// </summary>
		public bool HasAnyKey(
			params string[] keys)
		{
			foreach (var key1 in keys)
				if (HttpContext.Items.ContainsKey(key1))
					return true;
			return false;
		}


		/// <summary>
		/// Проверяет, что все ключи установлены
		/// </summary>
		public bool HasAllKeys(
			params string[] keys)
		{
			foreach (var key1 in keys)
				if (!HttpContext.Items.ContainsKey(key1))
					return false;
			return true;
		}


		/// <summary>
		/// Возвращает значение ключа как string
		/// </summary>
		public string GetKey(
			string key,
			string defaultValue = null)
		{
			return (string)HttpContext.Items[key] ?? defaultValue;
		}


		/// <summary>
		/// Возвращает значение ключа как int
		/// </summary>
		public int GetKeyInt(
			string key,
			int defaultValue = 0)
		{
			var value1 = HttpContext.Items[key];
			return value1 == null ? defaultValue : (int)value1;
		}


		/// <summary>
		/// Возвращает значение ключа как bool
		/// </summary>
		public bool GetKeyBool(
			string key)
		{
			var value1 = HttpContext.Items[key];
			return value1 != null && (bool)value1;
		}


		/// <summary>
		/// Возвращает значение ключа как DateTime
		/// </summary>
		public DateTime? GetKeyDateTime(
			string key,
			DateTime? defaultValue = null)
		{
			return (DateTime?)HttpContext.Items[key] ?? defaultValue;
		}


		/// <summary>
		/// Возвращает значение ключа как DateOnly
		/// </summary>
		public DateOnly? GetKeyDateOnly(
			string key,
			DateOnly? defaultValue = null)
		{
			return (DateOnly?)HttpContext.Items[key] ?? defaultValue;
		}


		/// <summary>
		/// Возвращает значение ключа как TimeOnly
		/// </summary>
		public TimeOnly? GetKeyTimeOnly(
			string key,
			TimeOnly? defaultValue = null)
		{
			return (TimeOnly?)HttpContext.Items[key] ?? defaultValue;
		}


		/* privates */


		private static string _getValueKeyOn(
			string key)
		{
			return $"{key}_on";
		}


		private static string _getValueKeyOff(
			string key)
		{
			return $"{key}_off";
		}

	}

}
