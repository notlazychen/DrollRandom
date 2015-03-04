#include <iostream>
using namespace std;
#include  <time.h>
#include "DrollRandom.h"
#include <fstream>

int main(){
	//int* i = new int[4];
	//cout << "hello world!"<<endl;
	//cout << __TIME__ << endl;
	ofstream file;
	file.open("filecpp.txt");

	cout << "please type the random seed: " << endl;
	int seed = 0;
	cin >> seed;
	DrollRandom* r1 = new DrollRandom(seed);
	//DrollRandom* r2 = new DrollRandom(1);
	cout << "please type the min value: " << endl;
	int min = 0;
	cin >> min;

	cout << "please type the max value: " << endl;
	int max = 0;
	cin >> max;

	cout << "how many times?" << endl;
	int times = 0;
	cin >> times;

	for (int i = 0; i < times; i++){
		int x1 = r1->Next(min, max);
		//int x2 = r2->Next(0, 100000000);
		//cout << "x1:" << x1 << ", x2:" << x2 << endl;
		//file << x1 << endl;
		//if (x1 != x2){
		//	cout <<"Error: "<< i<<endl;
		//	system("pause");
		//}
	}
	delete r1;/*,r2;*/

	file.close();

	cout << "over" << endl;

	system("pause");
	return 0;
}