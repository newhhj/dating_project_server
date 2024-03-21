function checkKeyDown() {
    if (event.keyCode === 13) {
        login();
    }
}

function login() {
    var userid = $('#userid').val();
    var passwd = $('#passwd').val();

    if (userid === undefined || userid.trim() === '') {
        alert('아이디를 입력하세요');
        return;
    }
    if (passwd === undefined || passwd.trim() === '') {
        alert('비밀번호를 입력하세요');
        return;
    }

    $.ajax({
        method: 'post',
        url: '/authentication/login',
        data: {
            userid: userid,
            passwd: passwd
        },
        success: (data) => {
            if (data.message !== undefined) {
                alert(data.message);
            }
            else {
                location.href = '/';
            }
        }
    });
}

function logout() {
    $.ajax({
        method: 'post',
        url: '/authentication/logout',
        success: (data) => {
            if (data.message !== undefined) {
                alert(data.message);
            }
            else {
                location.href = '/login';
            }
        }
    });
}

function pageReload() {
    location.reload();
}

function openerReload() {
    window.opener.location.reload();
}

function timecheck(obj) {
    document.onmousemove = resetTimeDelay;
    document.onkeypress = resetTimeDelay;

    function resetTimeDelay() {
        obj.invokeMethodAsync("ResetTimeDelay");
    }
}