#include "DrollRandom.h"


DrollRandom::DrollRandom()
{
	this->seed = (unsigned)__TIME__;
	unsigned initArray[] = { 0x123, 0x234, 0x345, 0x456 };
	_InitByArray(initArray, 4);
}


DrollRandom::~DrollRandom()
{
	delete mt;
}

DrollRandom::DrollRandom(unsigned seed)
{
	this->seed = seed;
	unsigned initArray[] = { 0x123, 0x234, 0x345, 0x456 };
	_InitByArray(initArray, 4);
}

int DrollRandom::Next(int maxValue)
{
	this->maxSize = maxValue;
	return (int)(_GenrandInt32() % maxSize);
}

int DrollRandom::Next(int minValue, int maxValue)
{
	int tmp = maxValue - minValue;
	return minValue + Next(tmp);
}

unsigned DrollRandom::_GenrandInt32()
{
	unsigned y;
	unsigned mag01[] = { 0x0, MATRIX_A };
	if (mti >= N)
	{ /* generate N words at one time */
		int kk;

		if (mti == N + 1)   /* if init_genrand() has not been called, */
			_InitGenrand(5489U); /* a default initial seed is used */

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

void DrollRandom::_InitByArray(unsigned init_key[], int key_length)
{
	int i, j, k;
	_InitGenrand(this->seed);
	//init_genrand(19650218);
	i = 1; j = 0;
	k = (N > key_length ? N : key_length);
	for (; k > 0; k--)
	{
		mt[i] = (unsigned)((unsigned)(mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525U)) + init_key[j] + j); /* non linear */
		mt[i] &= 0xffffffff; // for WORDSIZE > 32 machines
		i++; j++;
		if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
		if (j >= key_length) j = 0;
	}
	for (k = N - 1; k > 0; k--)
	{
		mt[i] = (unsigned)((unsigned)(mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941U)) - i); /* non linear */
		mt[i] &= 0xffffffffU; // for WORDSIZE > 32 machines
		i++;
		if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
	}

	mt[0] = 0x80000000U; // MSB is 1; assuring non-zero initial array
}

void DrollRandom::_InitGenrand(unsigned seed)
{
	mt[0] = seed & 0xffffffffU;
	for (mti = 1; mti < N; mti++)
	{
		mt[mti] = (unsigned)(1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
		// See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier. 
		// In the previous versions, MSBs of the seed affect   
		// only MSBs of the array mt[].                        
		// 2002/01/09 modified by Makoto Matsumoto             
		mt[mti] &= 0xffffffffU;
		// for >32 bit machines
	}
}