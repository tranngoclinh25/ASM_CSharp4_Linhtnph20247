window.SanPhamHomeController = function ($scope, $http) {
    $scope.lstDanhSach = [];
    $http.get("http://localhost:3000/sanphamhome").then(
        function (response) {
            console.log(response);
            $scope.lstDanhSach = response.data;
        },
        function (error) {
            console.log(error);
        }
    );
}