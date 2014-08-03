//
//  Sudoku.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//  Ref to web http://norvig.com/sudoku.html
//  Solve random sudoku

#ifndef __Game2__Sudoku__
#define __Game2__Sudoku__

#include <iostream>
#include <vector>
#include <algorithm>
#include <memory>

using namespace std;

class Possible {
private:
	vector<bool> _b;

public:
	Possible() : _b(9, true) {}
	bool   is_on(int i) const { return _b[i-1]; }
	int    count()      const { return std::count(_b.begin(), _b.end(), true); }
	void   eliminate(int i)   { _b[i-1] = false; }
	int    val()        const {
		auto it = find(_b.begin(), _b.end(), true);
		return (it != _b.end() ? 1 + (it - _b.begin()) : -1);
	}
	vector<int> remains();
	string str(int wth) const;
};

class Sudoku {
	vector<Possible> _cells;
	static vector< vector<int> > _group, _neighbors, _groups_of;

	bool     eliminate(int k, int val);
public:
	Sudoku(string s);
	static void init();
	Possible possible(int k) const { return _cells[k]; }
	bool		is_solved() const;
	bool		assign(int k, int val);
	int			least_count() const;
	void		write(ostringstream& o) const;
};

#endif /* defined(__Game2__Sudoku__) */
