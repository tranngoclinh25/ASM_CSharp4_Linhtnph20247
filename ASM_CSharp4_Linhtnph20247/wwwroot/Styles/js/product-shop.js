window.SanPhamShopController = function ($scope, $http, $routeParams) {
    $scope.lstDanhSach = [];
    $http.get("http://localhost:3000/sanphamshop").then(
        function (response) {
            console.log(response);
            $scope.lstDanhSach = response.data;
        },
        function (error) {
            console.log(error);
        }
    );
    $scope.onSubmit = function () {
        $http
            .post("http://localhost:3000/sanphamshop", {
                id: $scope.id,
                img: $scope.img,
                tensp: $scope.tensp,
                gia: $scope.gia,
            })
            .then(function (response) {
                if (response.status == 201) {
                    alert("Thêm thành công");
                }
            });
    };
    $scope.getFile = function () {
        var file = "./img/product/" + $scope.img;
        var reader = new FileReader();
        reader.onload = function (event) {
          $scope.img = event.target.result;
        };
        reader.readAsDataURL(file);
      };
    $scope.details = function (id) {
        $http
            .get("http://localhost:3000/sanphamshop/" + id).then(
                function (response) {
                    console.log(response);
                    if (response.statusText === "OK") {
                        $scope.id = response.data.id;
                        $scope.img = response.data.img;
                        $scope.tensp = response.data.tensp;
                        $scope.gia = response.data.gia;
                    }
                },
                function (error) {
                    console.log(error);
                }
            )
    }
    $scope.updateDanhSach = function () {
        $http
            .put("http://localhost:3000/sanphamshop/" + $scope.id, {
                id: $scope.id,
                img: $scope.img,
                tensp: $scope.tensp,
                gia: $scope.gia,
            })
            .then(function (response) {
                if (response.status == 200) {
                    alert("Sửa thành công");
                }
            });
    };
    $scope.deleteDanhSach = function (id) {
        $http
            .delete("http://localhost:3000/sanphamshop/" + id)
            .then(function (response) {
                if (response.status == 200) {
                    alert("Xóa thành công");
                }
            });
    };
    //product-details
    var id = $routeParams.id;
    console.log(id);
    $http.get("http://localhost:3000/sanphamshop/" + id).then(
        function (response) {
            if (response.statusText === "OK") {
                $scope.id = response.data.id;
                $scope.img = response.data.img;
                $scope.tensp = response.data.tensp;;
                $scope.gia = response.data.gia;
            }
        },
        function (errors) {
            console.log(errors);
        }
    );
    //shoping-cart
    $scope.lstShoping = [];
    $http.get("http://localhost:3000/shoping-cart").then(
        function (response) {
            console.log(response);
            $scope.lstShoping = response.data;
            calculateTotal();
        },
        function (error) {
            console.log(error);
        }
    );
    $scope.deleteProductCart = function (id) {
        $http
            .delete("http://localhost:3000/shoping-cart/" + id)
            .then(function (response) {
                if (response.status == 200) {
                    alert("Xóa thành công");
                }
            });
    };
    $scope.addShopingCart = function (id, img, tensp, gia) {
        $http
            .post("http://localhost:3000/shoping-cart", {
                id: id.data,
                img: img,
                tensp: tensp,
                soluong: 1,
                gia: gia,
            })
            .then(function (response) {
                if (response.status == 201) {
                    alert("Sản phẩm đã được thêm vào giỏ hàng");
                }
            });
    };
    $scope.addShopingCartDetail = function (id, img, tensp, soluong, gia) {
        $http
            .post("http://localhost:3000/shoping-cart", {
                id: id.data,
                img: img,
                tensp: tensp,
                soluong: soluong,
                gia: gia,
            })
            .then(function (response) {
                if (response.status == 201) {
                    alert("Sản phẩm đã được thêm vào giỏ hàng");
                }
            });
    };
    //Quantity
    $scope.quantity_tru = function (user) {
        console.log(user);
        user.soluong--;
        // $http.put("http://localhost:3000/shoping-cart/" + user.id, user)
    }
    $scope.quantity_cong = function (user) {
        console.log(user);
        user.soluong++;
        // $http.put("http://localhost:3000/shoping-cart/" + user.id, user)
    }
    function calculateTotal() {
        $scope.total = 0;
        angular.forEach($scope.lstShoping, function(item) {
          $scope.total += item.gia * item.soluong;
        });
      }
};