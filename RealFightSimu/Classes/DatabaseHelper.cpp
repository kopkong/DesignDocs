#include "DatabaseHelper.h"
#include "AssertConfigs.h"
#include "cocos2d.h"
#include "Utility.h"

DataBaseHelper::~DataBaseHelper()
{

}

DataBaseHelper& DataBaseHelper::Instance()
{
	static DataBaseHelper g_Instance;
	return g_Instance;
}

void DataBaseHelper::initDataBase()
{
	m_PDB = NULL;
	m_Result = sqlite3_open(DATABASE_FILE.c_str(),&m_PDB);
	if(m_Result != SQLITE_OK)
	{
		cocos2d::log("open database failed,  number%d",m_Result);
	}
}

void DataBaseHelper::addNewPlayer(std::string name)
{
	m_SQL = formatString("insert into Player(Name) values('%s')",name);
	m_Result = sqlite3_exec(m_PDB,m_SQL.c_str(),NULL,NULL,NULL);

	if(m_Result != SQLITE_OK)
	{
		cocos2d::log("insert data into table Player failed!");
	}
}

void DataBaseHelper::updatePlayerProperty()
{

}