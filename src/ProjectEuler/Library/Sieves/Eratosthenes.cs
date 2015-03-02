using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ProjectEuler
{
  public class Eratosthenes : IEnumerable<uint>
  {
    private List<uint> primes;
    private uint limit;

    public Eratosthenes(uint candidate)
    {
      limit = candidate;
      primes = new List<uint>();
      fill();
    }

    /// <summary>
    ///The idea is to narrow the sievingContainer to only look at 1/3 of the numbers. 
    ///(knot theory, http://www.scribd.com/doc/9935691/Prime-Numbers-without-Mystery-2
    /// </summary>
    /// <remarks>http://www.codeproject.com/Tips/228317/Find-Prime-Numbers-in-Csharp-Quickly</remarks>
    private void fill()
    {
      //Below are 6 columns of numbers. You can see columns 1 (1,7,13,..) and column 5 (5,11,17, ...)
      //Everything in column 2-4,6 can be ignored.
      //Primes are only in columns 1 and 5 (along with prime factors of only 2 primes and primes squared).
      //ie: 25 (5^2), 35 (7*5), 65 (5*13), 49, 91 (7 * 13)
      /*
          1	2	3	4	5	6
          7	8	9	10	11	12
          13	14	15	16	17	18
          19	20	21	22	23	24
          25	26	27	28	29	30
          31	32	33	34	35	36
          37	38	39	40	41	42
          43	44	45	46	47	48
          49	50	51	52	53	54
          55	56	57	58	59	60
          61	62	63	64	65	66
          67	68	69	70	71	72
          73	74	75	76	77	78
          79	80	81	82	83	84
          85	86	87	88	89	90
          91	92	93	94	95	96
          97	98	99	100	101	102
          103	104	105	106	107	108
          ....
      */

      var sieveContainer = new bool[limit + 1];

      sieveContainer[2] = true;
      sieveContainer[3] = true;

      //flag all factors of 6+/-1 as prime. 
      //this initializes our bit array that will be sieved.
      for (int i = 6; i <= limit; i += 6)
      {
        sieveContainer[i + 1] = true;
        sieveContainer[i - 1] = true;
      }

      //Starting at marker (ie: 5), square it and flag that as not prime.
      //Then add marker * 6 to marker starting at marker^2. (ie: 5 * 5 = 25, add 5*6 = 30, so 30+25=55, 85,...). Flag those as not prime
      //Next, add marker * 6 to marker starting at marker. (ie: 5 * 6 + 5 = 35, 65, 95. Flag these as not prime
      //Repeat starting with the next marker not sieved off. ie: 7, 11, 13
      long marker, factor;
      marker = 5;
      while (marker * 2 <= limit)
      {
        //sieve the prime^2
        while (marker * marker <= limit)
        {
          sieveContainer[(int)(marker * marker)] = false;
          break;
        }

        //sieve marker plus factors of marker * 6, starting at p^2
        factor = (marker * marker) + (marker * 6);
        while (factor <= limit)
        {
          sieveContainer[(int)factor] = false;
          factor += marker * 6;
        }

        //sieve marker plus factors of marker * 6
        factor = marker * 6 + marker;
        while (factor <= limit)
        {
          sieveContainer[(int)factor] = false;
          factor += marker * 6;
        }

        while (!sieveContainer[(int)++marker]) ;
      }

      marker = 0;
      while (++marker < limit)
      {
        if (sieveContainer[(int)marker])
        {
          primes.Add((uint)marker);
        }
      }
    }

    public IEnumerator<uint> GetEnumerator()
    {
      foreach (var p in primes)
        yield return p;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
