#include "GameBaseEntity.h"


int GameBaseEntity::m_NextValidID = 0;


// �ڹ��캯���е����������ȷ��ÿ�ζ�����ȷ������ID
void GameBaseEntity::setID(int val)
{
	// ȷ��ÿһ��ID��С����һ����ЧID
	CC_ASSERT( (val >= m_NextValidID) && "Invalid ID, IDС����һ����ЧID");

	m_ID = val;

	m_NextValidID = m_ID + 1 ;
};