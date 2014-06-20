#include "player.h"
#include "cocos2d.h"
#include "playerDataKeyConfig.h"
#include "Marco.h"

PlayerDataMgr& PlayerDataMgr::Instance()
{
	static PlayerDataMgr singleton;
	return singleton;
}

PlayerDataMgr::~PlayerDataMgr()
{
	delete m_Player;
}

void PlayerDataMgr::initPlayerData()
{
	m_Player = new Player();

	m_Player->m_Name = GetStringByKey(PLAYER_DATAKEY_NAME);
	if(m_Player->m_Name.length() == 0)
		m_Player->m_Name = "Player";

	m_Player->m_Level = GetIntergerByKey(PLAYER_DATAKEY_LEVEL);
	if(m_Player->m_Level == 0)
		m_Player->m_Level = 1;

	m_Player->m_EXP = GetIntergerByKey(PLAYER_DATAKEY_EXP);
	m_Player->m_VIPLEVEL = GetIntergerByKey(PLAYER_DATAKEY_VIPLEVEL);
	m_Player->m_CurrentCoin = GetIntergerByKey(PLAYER_DATAKEY_COIN);
	m_Player->m_CurrentDiamonds = GetIntergerByKey(PLAYER_DATAKEY_DIAMOND);
	m_Player->m_CurrentHeroSouls = GetIntergerByKey(PLAYER_DATAKEY_HEROSOUL);
	m_Player->m_Honor = GetIntergerByKey(PLAYER_DATAKEY_HONOR);
	m_Player->m_Fame = GetIntergerByKey(PLAYER_DATAKEY_FAME);
	m_Player->m_Energy = GetIntergerByKey(PLAYER_DATAKEY_ENERGY);

}

void PlayerDataMgr::savePlayerData()
{
	SetStringByKey(PLAYER_DATAKEY_NAME,m_Player->m_Name);
	SetIntergerByKey(PLAYER_DATAKEY_LEVEL,m_Player->m_Level);
	SetIntergerByKey(PLAYER_DATAKEY_EXP,m_Player->m_EXP);
	SetIntergerByKey(PLAYER_DATAKEY_VIPLEVEL,m_Player->m_VIPLEVEL);
	SetIntergerByKey(PLAYER_DATAKEY_COIN,m_Player->m_CurrentCoin);
	SetIntergerByKey(PLAYER_DATAKEY_DIAMOND,m_Player->m_CurrentDiamonds);
	SetIntergerByKey(PLAYER_DATAKEY_HEROSOUL,m_Player->m_CurrentHeroSouls);
	SetIntergerByKey(PLAYER_DATAKEY_HONOR,m_Player->m_Honor);
	SetIntergerByKey(PLAYER_DATAKEY_FAME,m_Player->m_Fame);
	SetIntergerByKey(PLAYER_DATAKEY_ENERGY,m_Player->m_Energy);
}