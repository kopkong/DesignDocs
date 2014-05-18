//
//  SudokuFactory.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//
//

#include "SudokuFactory.h"

#include <memory>
#include <string>
#include <sstream>
#include <map>
#include <algorithm>
#include <random>
#include <ctime>

using namespace std;

int myrandom (int i) { return std::rand()%i;}

string intToStr(int i){
	char buf[256];
	sprintf(buf,"%d",i);
	string s = buf;
	return s;
}

SudokuFactory::~SudokuFactory()
{
}

SudokuFactory* SudokuFactory::getInstance()
{
	static SudokuFactory s_SudokuFactory;
	return &s_SudokuFactory;
}

Sudoku* SudokuFactory::solve(Sudoku* S) {
	if (S == nullptr || S->is_solved()) {
		return S;
	}
	int k = S->least_count();
	Possible p = S->possible(k);
	for (int i = 1; i <= 9; i++) {
		if (p.is_on(i)) {
			Sudoku* S1(new Sudoku(*S));
			if (S1->assign(k, i)) {
				if (auto S2 = solve(std::move(S1))) {
					return S2;
				}
			}
		}
	}
	return NULL;
}


SudokuProduction SudokuFactory::generateSudoku()
{
	CCASSERT(_workingLevel>=1,"Factory working level is invalid");

	std::string line = random_puzzle(getAssignments());

	//std::string test1 = "4.....8.5.3..........7......2.....6.....8.4......1.......6.3.7.5..2.....1.4......";
	//std::string test2 = "1....9.6.2......3.3.......14132...9.6..49.1..97..1..4.83.......7.9...6585........";
	//size_t len1 = test1.length();
	//size_t len2 = test2.length();

	SudokuProduction production;
	production.Initials = string(line);

	Sudoku::init();
	if (auto S = solve(new Sudoku(line))) {
		std::ostringstream os;
		S->write(os);
		production.Answers = os.str();
		cout << os;

		delete S;
	} else {
		cout << "No solution";
	}
	cout << endl;

	return production;

}

int SudokuFactory::getAssignments()
{
	CCASSERT(_workingLevel>=1,"Factory working level is invalid");

	switch (_workingLevel)
	{
	case 1:
		return 28;
	case 2:
		return 24;
	case 3:
		return 20;
	default:
		return 28;
	}

	return 0;
}

string SudokuFactory::random_puzzle(int N)
{ 
	std::srand(unsigned(std::time(0)));

	Sudoku::init();
	Sudoku su(".................................................................................");
	string puzzle("");
	vector<int> keys;

	for(int i=0;i<81;i++)
	{
		keys.push_back(i);
	}

	std::random_shuffle(keys.begin(),keys.end(),myrandom);
	std::set<int> uniqueDigits;
	for(vector<int>::iterator it = keys.begin(); it!=keys.end() ; it ++)
	{
		int sizeDigits = su.possible(*it).count();
		int r = myrandom(sizeDigits);
		vector<int> l = su.possible(*it).remains();
		int v =l[r];
		if(!su.assign(*it,v))
			break;

		unsigned int alreadyGetAssignments(0);
		for(vector<int>::iterator it1 = keys.begin(); it1 != keys.end(); it1++)
		{
			if(su.possible(*it1).count() == 1)
			{
				alreadyGetAssignments ++;
				uniqueDigits.insert(su.possible(*it1).val());
			}
		}

		if(alreadyGetAssignments >= N && uniqueDigits.size() >= 8)
		{
			for(int i =0;i<81;i++)
			{
				if(su.possible(i).count() == 1)
					puzzle += su.possible(i).str(1);
				else
					puzzle += ".";
			}

			return puzzle;
		}
	}

	return random_puzzle(N);


	return "";
}