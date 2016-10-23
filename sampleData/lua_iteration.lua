local function explode(div,str)
    if (div=='') then return false end
    local pos,arr = 0,{}
    for st,sp in function() return string.find(str,div,pos,true) end do
        table.insert(arr,string.sub(str,pos,st-1))
        pos = sp + 1
    end
    table.insert(arr,string.sub(str,pos))
    return arr
end

local sum = 0
local matches = redis.call('smembers', 'urn:battle:k:0:RightOrMismatch')

for _,key in ipairs(matches) do
    --local val = redis.call('GET', key)
	
	local keyPai = explode('|', key)
	--local keyPai = string.gmatch(key, '([^|]+)')

--for word in string.gmatch(key, '([^|]+)') do
--    print(word)
--end
	
	local id = keyPai[1]
	local isMember = redis.call('SISMEMBER', 'urn:battle:p:0:RightOnly', id)
	if isMember == 1 then
		-- redis.call('SADD', 'urn:battle:DONE:0:RightOnly', key)
		redis.call('SMOVE', 'urn:battle:k:0:RightOrMismatch', 'urn:battle:DONE:0:RightOnly', key)
	end
    sum = sum + 1
end

return sum



