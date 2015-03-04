using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrollRandom
{
    public class DrollRandom
    {
        //private static MersenneTwister  _twister = new MersenneTwister();


        private const int N = 624;
        private const int M = 397;
        private const uint MATRIX_A = 0x9908b0dfU;   // constant vector a
        private const uint UPPER_MASK = 0x80000000U; // most significant w-r bits
        private const uint LOWER_MASK = 0x7fffffffU; // least significant r bits


        private uint seed;

        private int returnLength;

        private int maxSize;

        // the array for the state vector
        private uint[] mt = new uint[N];

        private int mti = N + 1;

        public DrollRandom()
        {
            this.seed = (uint)DateTime.Now.Millisecond;
            var initArray = new uint[] { 0x123, 0x234, 0x345, 0x456 };
            InitByArray(initArray, initArray.Length);
        }

        public DrollRandom(uint seed)
        {
            this.seed = seed;
            var initArray = new uint[] { 0x123, 0x234, 0x345, 0x456 };
            InitByArray(initArray, initArray.Length);
        }

        public uint Seed
        {
            get { return seed; }
        }

        //public static MersenneTwister Twister
        //{
        //    get { return _twister; }
        //}

        public int[] Twist(uint seed, int returnLength, int maxSize)
        {
            uint[] initArray;
            int[] returnArray;

            this.seed = seed;
            this.returnLength = returnLength;
            this.maxSize = maxSize;

            mti = N + 1;
            mt = new uint[N];

            initArray = new uint[] { 0x123, 0x234, 0x345, 0x456 };
            returnArray = new int[returnLength];
            InitByArray(initArray, initArray.Length);
            for (int i = 0; i < returnLength; i++)
            {
                returnArray[i] = (int)(GenrandInt32() % maxSize);
            }
            return returnArray;
        }

        /// <summary>
        /// 从0到maxValue,不包括maxValue
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public int Next(int maxValue)
        {
            this.maxSize = maxValue;
            return (int)(GenrandInt32() % maxSize);
        }

        /// <summary>
        /// 从minValue到maxValue,不包括maxValue
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public int Next(int minValue, int maxValue)
        {
            int tmp = maxValue - minValue;
            return minValue + Next(tmp);
        }

        private uint GenrandInt32()
        {
            uint y;
            uint[] mag01 = new uint[] { 0x0, MATRIX_A };
            if (mti >= N)
            { /* generate N words at one time */
                int kk;

                if (mti == N + 1)   /* if init_genrand() has not been called, */
                    InitGenrand(5489U); /* a default initial seed is used */

                for (kk = 0; kk < N - M; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1U];
                }
                for (; kk < N - 1; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1U];
                }
                y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
                mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1U];

                mti = 0;
            }

            y = mt[mti++];

            // Tempering
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680U;
            y ^= (y << 15) & 0xefc60000U;
            y ^= (y >> 18);

            return y;
        }

        private void InitByArray(uint[] init_key, int key_length)
        {
            int i, j, k;
            InitGenrand(Seed);
            //init_genrand(19650218);
            i = 1; j = 0;
            k = (N > key_length ? N : key_length);
            for (; k > 0; k--)
            {
                mt[i] = (uint)((uint)(mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525U)) + init_key[j] + j); /* non linear */
                mt[i] &= 0xffffffff; // for WORDSIZE > 32 machines
                i++; j++;
                if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
                if (j >= key_length) j = 0;
            }
            for (k = N - 1; k > 0; k--)
            {
                mt[i] = (uint)((uint)(mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941U)) - i); /* non linear */
                mt[i] &= 0xffffffffU; // for WORDSIZE > 32 machines
                i++;
                if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
            }

            mt[0] = 0x80000000U; // MSB is 1; assuring non-zero initial array
        }

        // initializes mt[N] with a seed
        private void InitGenrand(uint seed)
        {
            mt[0] = seed & 0xffffffffU;
            for (mti = 1; mti < N; mti++)
            {
                mt[mti] = (uint)(1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
                // See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier. 
                // In the previous versions, MSBs of the seed affect   
                // only MSBs of the array mt[].                        
                // 2002/01/09 modified by Makoto Matsumoto             
                mt[mti] &= 0xffffffffU;
                // for >32 bit machines
            }
        }
    }
}

