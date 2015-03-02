using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  /// <summary>
  /// Represents a collection of values tied to Int64 keys.
  /// </summary>
  /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
  [DebuggerDisplay("Count = {Count}")]
  public class Dictionary64<TValue> : IDictionary<long, TValue>
  {
    private Dictionary<int, Dictionary<int, KeyValuePair<long, TValue>>> dictionary;

    /// <summary>
    /// Initializes a new empty instance of the Dictionary64 class.
    /// </summary>
    public Dictionary64()
    {
      dictionary = new Dictionary<int, Dictionary<int, KeyValuePair<long, TValue>>>();
    }

    #region IDictionary<long,TValue> Members

    /// <summary>
    /// Gets a collection containing the keys.
    /// </summary>
    public ICollection<long> Keys
    {
      get
      {
        return dictionary
          .SelectMany(kvp => kvp.Value.Values)
          .Select(kvp => kvp.Key)
          .ToList();
      }
    }

    /// <summary>
    /// Gets a collection containing the values.
    /// </summary>
    public ICollection<TValue> Values
    {
      get
      {
        return dictionary
          .SelectMany(kvp => kvp.Value.Values)
          .Select(kvp => kvp.Value)
          .ToList();
      }
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to get or set.</param>
    /// <returns>
    /// The value associated with the specified key. If the specified key is not
    /// found, a get operation throws a System.Collections.Generic.KeyNotFoundException,
    /// and a set operation creates a new element with the specified key.
    /// </returns>
    public TValue this[long key]
    {
      get
      {
        var keys = _keys(key);
        var key1 = keys[0];
        var key2 = keys[1];
        return dictionary[key1][key2].Value;
      }
      set
      {
        var keys = _keys(key);
        var key1 = keys[0];
        var key2 = keys[1];

        Dictionary<int, KeyValuePair<long, TValue>> table;
        if (!dictionary.TryGetValue(key1, out table))
        {
          table = new Dictionary<int, KeyValuePair<long, TValue>>();
          dictionary[key1] = table;
        }

        table[key2] = new KeyValuePair<long, TValue>(key, value);
      }
    }

    /// <summary>
    /// Gets the number of key/value pairs contained in the 
    /// dictionary.
    /// </summary>
    public int Count
    {
      get { return dictionary.Sum(k => k.Value.Count); }
    }

    /// <summary>
    /// Always returns false.
    /// </summary>
    public bool IsReadOnly
    {
      get { return false; }
    }

    /// <summary>
    /// Adds the specified key and value to the dictionary.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">
    /// The value of the element to add. The value can be null for 
    /// reference types.
    /// </param>
    public void Add(long key, TValue value)
    {
      var keys = _keys(key);
      var key1 = keys[0];
      var key2 = keys[1];

      Dictionary<int, KeyValuePair<long, TValue>> table;
      if (!dictionary.TryGetValue(key1, out table))
      {
        table = new Dictionary<int, KeyValuePair<long, TValue>>();
        dictionary[key1] = table;
      }

      table.Add(key2, new KeyValuePair<long, TValue>(key, value));
    }

    /// <summary>
    /// Adds the specified key and value to the dictionary.
    /// </summary>
    /// <param name="item">The key and value element to add.</param>
    public void Add(KeyValuePair<long, TValue> item)
    {
      Add(item.Key, item.Value);
    }

    /// <summary>
    /// Determines whether the Dictionary64 contains the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the Dictionary64.</param>
    /// <returns>
    /// true if the Dictionary64 contains an element with the specified key; 
    /// otherwise, false.
    /// </returns>
    public bool ContainsKey(long key)
    {
      var keys = _keys(key);
      var key1 = keys[0];
      var key2 = keys[1];

      Dictionary<int, KeyValuePair<long, TValue>> table;
      if (!dictionary.TryGetValue(key1, out table))
      {
        return false;
      }

      return table.ContainsKey(key2);
    }

    /// <summary>
    /// Determines whether the Dictionary64 contains the specified key and value.
    /// </summary>
    /// <param name="item">The key and value to locate in the Dictionrary64.</param>
    /// <returns>
    /// true if the Dictionary64 contains an element with the specified key and value;
    /// otherwise, false.
    /// </returns>
    public bool Contains(KeyValuePair<long, TValue> item)
    {
      var keys = _keys(item.Key);
      var key1 = keys[0];
      var key2 = keys[1];

      Dictionary<int, KeyValuePair<long, TValue>> table;
      if (!dictionary.TryGetValue(key1, out table))
      {
        return false;
      }

      KeyValuePair<long, TValue> kvp;
      if (!table.TryGetValue(key2, out kvp))
      {
        return false;
      }

      return EqualityComparer<TValue>.Default.Equals(kvp.Value, item.Value);
    }

    /// <summary>
    /// Removes the value with the specified key from the Dictionary64.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>
    /// true if the element is successfully found and removed; otherwise, false. 
    /// This method returns false if key is not found in the Dictionary64.
    /// </returns>
    public bool Remove(long key)
    {
      var keys = _keys(key);
      var key1 = keys[0];
      var key2 = keys[1];

      Dictionary<int, KeyValuePair<long, TValue>> table;
      if (!dictionary.TryGetValue(key1, out table))
      {
        return false;
      }

      return table.Remove(key2);
    }

    /// <summary>
    /// Removes the value with the specified key from the Dictionary64.
    /// </summary>
    /// <param name="item">The key of the element to remove.</param>
    /// <returns>
    /// true if the element is successfully found and removed; otherwise, false. 
    /// This method returns false if key is not found in the Dictionary64.
    /// </returns>
    public bool Remove(KeyValuePair<long, TValue> item)
    {
      return Remove(item.Key);
    }

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified
    /// key, if the key is found; otherwise, the default value for the type of the
    /// value parameter. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// true if the Dictionary64 contains an element with the specified key; 
    /// otherwise, false.
    /// </returns>
    public bool TryGetValue(long key, out TValue value)
    {
      value = default(TValue);

      var keys = _keys(key);
      var key1 = keys[0];
      var key2 = keys[1];

      Dictionary<int, KeyValuePair<long, TValue>> table;
      if (!dictionary.TryGetValue(key1, out table))
      {
        return false;
      }

      KeyValuePair<long, TValue> kvp;
      if (table.TryGetValue(key2, out kvp))
      {
        value = kvp.Value;
        return true;
      }

      return false;
    }

    /// <summary>
    /// Removes all keys and values from the Dictionary64.
    /// </summary>
    public void Clear()
    {
      foreach (var kvp in dictionary)
      {
        kvp.Value.Clear();
      }
      dictionary.Clear();
    }

    /// <summary>
    /// Copies the collection of keys and values to the specified array.
    /// </summary>
    /// <param name="array">The array to copy to.</param>
    /// <param name="arrayIndex">The index to start copying to.</param>
    public void CopyTo(KeyValuePair<long, TValue>[] array, int arrayIndex)
    {
      var kvps = dictionary.SelectMany(k => k.Value.Values);
      foreach (var item in kvps)
      {
        array[arrayIndex++] = item;
      }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the Dictionary64.
    /// </summary>
    /// <returns>An object for iterating the Dictionary64.</returns>
    public IEnumerator<KeyValuePair<long, TValue>> GetEnumerator()
    {
      return dictionary
        .SelectMany(k => k.Value.Values)
        .GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the Dictionary64.
    /// </summary>
    /// <returns>An object for iterating the Dictionary64.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return dictionary
        .SelectMany(k => k.Value.Values)
        .GetEnumerator();
    }

    #endregion

    private static int[] _keys(long l)
    {
      var lower = (int)(l >> 32);
      var upper = (int)(l & 0xFFFFFFFF);

      return new[] {upper, lower};
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool Insert(long key, TValue value)
    {
      TValue existing;
      if (TryGetValue(key, out existing))
      {
        return false;
      }

      Add(key, value);
      return true;
    }
  }
}