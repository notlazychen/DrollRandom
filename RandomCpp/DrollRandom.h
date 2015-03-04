#pragma once
class DrollRandom
{
private:
	const int N = 624;
	const int M = 397;
	const unsigned int MATRIX_A = 0x9908b0dfU;   // constant vector a
	const unsigned int UPPER_MASK = 0x80000000U; // most significant w-r bits
	const unsigned int LOWER_MASK = 0x7fffffffU; // least significant r bits

	unsigned seed;
	int returnLength;
	int maxSize;
	unsigned int* mt = new unsigned int[N];
	int mti = N + 1;

public:
	DrollRandom();
	DrollRandom(unsigned seed);
	~DrollRandom();

	int Next(int maxValue);
	int Next(int minValue, int maxValue);

	unsigned GetSeed()
	{
		return seed;
	}

private:
	unsigned _GenrandInt32();
	void _InitByArray(unsigned init_key[], int key_length);
	void _InitGenrand(unsigned seed);

};

