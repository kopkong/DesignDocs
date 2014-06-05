#include "GameBaseEntity.h"


int GameBaseEntity::m_NextValidID = 0;


// 在构造函数中调用这个方法确保每次都能正确的设置ID
void GameBaseEntity::setID(int val)
{
	// 确保每一个ID不小于下一个有效ID
	CC_ASSERT( (val >= m_NextValidID) && "Invalid ID, ID小于下一个有效ID");

	m_ID = val;

	m_NextValidID = m_ID + 1 ;
};