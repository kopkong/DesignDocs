//
//  SudokuFactory.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//
//

#ifndef __Game2__SudokuFactory__
#define __Game2__SudokuFactory__

#include <iostream>
#include "Sudoku.h"
#include "Cocos2d.h"
USING_NS_CC;

// Factory produce sudoku production that can be used in game
struct SudokuProduction
{
	std::string Initials;
	std::string Answers;

	std::string getAnswerAtIndex(int i)
	{
		string s ;
		s.insert(s.begin(),Answers[i]);
		return s;
	}
};

class SudokuFactory
{
private:
	Sudoku* solve(Sudoku*);
	int _workingLevel;
	int getAssignments();
	string random_puzzle(int N);
protected:
	SudokuFactory(void):_workingLevel(0){};
	~SudokuFactory();

public:
	static SudokuFactory* getInstance();
	void setWorkingLevel(int lv){_workingLevel = lv;}
	SudokuProduction generateSudoku();
};

#endif /* defined(__Game2__SudokuFactory__) */
