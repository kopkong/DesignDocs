#ifndef RealFightSimu_DatabaseHelper_h
#define RealFightSimu_DatabaseHelper_h

#include "sqlite-amalgamation-3080500/sqlite3.h"
#include <string>
#include "player.h"
#include "SlotMgr.h"

class DataBaseHelper
{
private:
	int m_Result;
	sqlite3 *m_PDB ;
	std::string m_SQL;

protected:
	~DataBaseHelper();

public:
	static DataBaseHelper& Instance();
	
	void initDataBase();
	
	void addNewPlayer(std::string);
	
	void updatePlayerProperty(Player*);
	
	void loadPlayerProperty(Player*);

	void updatePlayerSlots();

	void loadPlayerSlots();
};


#endif