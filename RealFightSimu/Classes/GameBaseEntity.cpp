#include "GameBaseEntity.h"


int GameBaseEntity::m_NextValidID = 0;


// 设置ID
void GameBaseEntity::setID(int val)
{
	// 检查ID是否有效
	CC_ASSERT( (val >= m_NextValidID) && "Invalid ID");

	m_ID = val;

	m_NextValidID = m_ID + 1 ;
};