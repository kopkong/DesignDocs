#ifndef __BASE_STATE_H__
#define __BASE_STATE_H__

template<class entity_type>
class BaseState
{
public:

	virtual ~State(){}

	// �ս���״̬ʱִ��
	virtual void enter(entity_type*) = 0;

	// ����״̬ʱִ��
	virtual void execute(entity_type*) = 0;

	// �˳�״̬ʱִ��
	virtual void exit(entity_type*) = 0;

}



#endif