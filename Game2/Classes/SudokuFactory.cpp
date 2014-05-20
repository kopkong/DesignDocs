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

	SudokuProduction production;
	production.Initials = string(line);

	Sudoku::init();
	if (auto S = solve(new Sudoku(line))) {
		std::ostringstream os;
		S->write(os);
		production.Answers = os.str();
		delete S;
	} else {
        log("No solution");
	}
    
    log("Answer is %s,length is %d",production.Answers.c_str(),production.Answers.length());
    
    
    CCASSERT(production.Initials.length()==81,"Initial length is not 81");
    CCASSERT(production.Answers.length()==81,"Answer length is not 81");

	return production;

}

int SudokuFactory::getAssignments()
{
	CCASSERT(_workingLevel>=1,"Factory working level is invalid");

	switch (_workingLevel)
	{
	case 1:
		return 31;
	case 2:
		return 21;
	case 3:
		return 17;
	default:
		return 25;
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
        
        log("Assignments is %d",alreadyGetAssignments);
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