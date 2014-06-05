#ifndef __BASE_STATE_H__
#define __BASE_STATE_H__

template<class entity_type>
class BaseState
{
public:

	virtual ~State(){}

	// 刚进入状态时执行
	virtual void enter(entity_type*) = 0;

	// 正常状态时执行
	virtual void execute(entity_type*) = 0;

	// 退出状态时执行
	virtual void exit(entity_type*) = 0;

}



#endif