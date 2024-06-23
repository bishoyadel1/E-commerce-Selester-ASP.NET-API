
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/Core/Home/home/home.component';
import { ContactusComponent } from './Components/Core/contactus/contactus.component';
import { LoginComponent } from './Components/Core/login/login.component';
import { RegisterComponent } from './Components/Core/register/register.component';
import { BlogComponent } from './Components/Core/blog/blog.component';
import { ProfileComponent } from './Components/Core/profile/profile.component';
import { ProductListComponent } from './Components/Core/product-list/product-list.component';
import { CartComponent } from './Components/Core/cart/cart.component';
import { NotfoundComponent } from './Components/Core/notfound/notfound.component';
import { AddressComponent } from './Components/Core/user profile/address/address.component';
import { AddAddressComponent } from './Components/Core/user profile/add-address/add-address.component';
import { GeneralInformationComponent } from './Components/Core/user profile/general-information/general-information.component';
import { OrderListComponent } from './Components/Core/user profile/order-list/order-list.component';
import { OrderdetailsComponent } from './Components/Core/user profile/orderdetails/orderdetails.component';
import { authGuard } from './Guards/auth.guard';
import { authGenricGuard } from './Guards/authGenric.guard';
import { authDeliveyGuard } from './Guards/authDelivery.guard';
import { authSellerGuard } from './Guards/authSeller.guard';
import { WishlistUserDashboardComponent } from './Components/Core/wishlist-user-dashboard/wishlist-user-dashboard.component';
import { FavoritesUserDashboardComponent } from './Components/Core/favorites-user-dashboard/favorites-user-dashboard.component';
import { ProductDetailsComponent } from './Components/Core/product-details/product-details.component';
import { CheckoutComponent } from './Components/Core/checkout/checkout.component';
import { DashhomeComponent } from './Components/Core/dashboard/dashhome/dashhome.component';
import { ProductsListDasboardComponent } from './Components/Core/dashboard/products-list-dasboard/products-list-dasboard.component';
import { ProductFormComponent } from './Components/Core/dashboard/product-form/product-form.component';
import { CategoryListDashboardComponent } from './Components/Core/dashboard/categories/category-list-dashboard/category-list-dashboard.component';
import { BrandListDashboardComponent } from './Components/Core/dashboard/brands/brand-list-dashboard/brand-list-dashboard.component';
import { UsersListAdminDashboardComponent } from './Components/Core/dashboard/users/users-list-admin-dashboard/users-list-admin-dashboard.component';
import { OrdersListDashboardComponent } from './Components/Core/dashboard/orders/orders-list-dashboard/orders-list-dashboard.component';
import { ReviewsListAdminDashboardComponent } from './Components/Core/dashboard/reviews/reviews-list-admin-dashboard/reviews-list-admin-dashboard.component';
import { ContactAdminDashboardComponent } from './Components/Core/dashboard/contactus/contact-admin-dashboard/contact-admin-dashboard.component';
import { EditProductFormComponent } from './Components/Core/dashboard/edit-product-form/edit-product-form.component';
import { ChangepasswordComponent } from './Components/Core/user profile/changepassword/changepassword.component';
import { AdminOrderDetailsComponent } from './Components/Core/dashboard/orders/admin-order-details/admin-order-details.component';
import { AddUserComponent } from './Components/Core/dashboard/users/add-user/add-user.component';
import { AdminReportsComponent } from './Components/Core/dashboard/reports/admin-reports/admin-reports.component';
import { PhonesComponent } from './Components/Core/phones/phones.component';
import { AddphoneComponent } from './Components/Core/addphone/addphone.component';
import { ResetpasswordComponent } from './Components/Core/resetpassword/resetpassword.component';
import { ForgetpasswordComponent } from './Components/Core/forgetpassword/forgetpassword.component';
import { ConfirmemailComponent } from './Components/Core/confirmemail/confirmemail.component';
import { CheckemailComponent } from './Components/Core/checkemail/checkemail.component';

const routes: Routes = [
  { path: 'contactus', component: ContactusComponent },
  { path: 'blog', component: BlogComponent },
  { path: 'login', component: LoginComponent, canActivate: [authGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [authGuard] },
  { path: 'resetPassword', component: ResetpasswordComponent, canActivate: [authGuard] },
  { path: 'confirmEmail', component: ConfirmemailComponent, canActivate: [authGuard] },
  { path: 'checkEmail', component: CheckemailComponent, canActivate: [authGuard] },
  { path: 'forgetPassword', component: ForgetpasswordComponent, canActivate: [authGuard] },
  { path: 'profile', component: GeneralInformationComponent, canActivate: [authGuard] },
  { path: 'Changepassword', component: ChangepasswordComponent, canActivate: [authGuard] },
  { path: 'address', component: AddressComponent, canActivate: [authGuard] },
  { path: 'phones', component: PhonesComponent, canActivate: [authGuard] },
  { path: 'phones/add', component: AddphoneComponent, canActivate: [authGuard] },
  { path: 'wishlist', component: WishlistUserDashboardComponent, canActivate: [authGuard] },
  { path: 'favorite', component: FavoritesUserDashboardComponent, canActivate: [authGuard] },
  { path: 'address/add', component: AddAddressComponent, canActivate: [authGuard] },
  { path: 'orders', component: OrderListComponent, canActivate: [authGuard] },
  { path: 'orderdetails/:id', component: OrderdetailsComponent, canActivate: [authGuard] },
  { path: 'cart', component: CartComponent, canActivate: [authGuard] },
  { path: 'products', component: ProductListComponent },
  { path: 'products/details/:id', component: ProductDetailsComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'admin/home', component: DashhomeComponent, canActivate: [authGenricGuard] },
  { path: 'admin/Reports', component: AdminReportsComponent, canActivate: [authSellerGuard] },
  { path: 'admin/products', component: ProductsListDasboardComponent, canActivate: [authSellerGuard] },
  { path: 'products/new', component: ProductFormComponent, canActivate: [authSellerGuard] },
  { path: 'products/edit/:id', component: EditProductFormComponent, canActivate: [authSellerGuard] },
  { path: 'admin/categories', component: CategoryListDashboardComponent, canActivate: [authGuard] },
  { path: 'admin/brands', component: BrandListDashboardComponent, canActivate: [authSellerGuard] },
  { path: 'admin/users', component: UsersListAdminDashboardComponent, canActivate: [authGuard] },
  { path: 'admin/users/new', component: AddUserComponent, canActivate: [authGuard] },
  { path: 'admin/orders', component: OrdersListDashboardComponent, canActivate: [authDeliveyGuard] },
  { path: 'admin/order/details/:id', component: AdminOrderDetailsComponent, canActivate: [authDeliveyGuard] },
  { path: 'admin/reviews', component: ReviewsListAdminDashboardComponent, canActivate: [authSellerGuard] },
  { path: 'admin/ContactUs', component: ContactAdminDashboardComponent, canActivate: [authGuard] },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: '**', component: NotfoundComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
