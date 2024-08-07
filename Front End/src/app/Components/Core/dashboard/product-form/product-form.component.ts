import { Iproductadd } from './../../../../Interfaces/product/iproductadd';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Ibrandreturn } from 'src/app/Interfaces/brand/ibrandreturn';
import { Icategoryreturn } from 'src/app/Interfaces/category/icategoryreturn';
import { Iproduct } from 'src/app/Interfaces/iproduct';
import { Iwarranty } from 'src/app/Interfaces/iwarranty';
import { Iproductreturn } from 'src/app/Interfaces/product/iproductreturn';
import { BrandService } from 'src/app/Services/dashboard/brand.service';
import { CategoryService } from 'src/app/Services/dashboard/category.service';
import { ProductlistService } from 'src/app/Services/productlist.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent {

  fg !: FormGroup;

  selectedImage: File | null = null;

  productId: number = 0;
  images: File[] = [];
  warranties: Iwarranty[] = [];
  categories: Icategoryreturn[] = [];
  brands: Ibrandreturn[] = [];

  constructor(private fb: FormBuilder,
    private productapi: ProductlistService,
    private catService: CategoryService,
    private brandService: BrandService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {

    this.productId = this.activatedRoute.snapshot.params['id'];

    //#region FormGroup
    this.fg = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(100)]],
      price: ['', [Validators.required, Validators.min(1000), Validators.max(80000)]],
      condition: [1, [Validators.required, Validators.min(0), Validators.max(1)]],
      stockQuantity: ['', [Validators.required, Validators.min(0), Validators.max(100)]],
      discount: [0, [Validators.required, Validators.min(0), Validators.max(70)]],
      model: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      color: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      ProductDetailone: ['', [Validators.required, Validators.min(16), Validators.max(1000)]],
      ProductDetailtwo: ['', [Validators.required, Validators.min(8), Validators.max(64)]],
      ProductDetailthree: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      ProductDetailfour: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      ProductDetailfive: ['', [Validators.required]],
      ProductDetailsix: ['', [Validators.required, Validators.min(16), Validators.max(10000)]],
      ProductDetailsiven: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      categoryID: [1, [Validators.required, Validators.min(1)]],
      brandID: [1, [Validators.required]],
      warranties: [''],
      images: ['']
      //#endregion

    })

    // ---------------- [ Get All Categories ]
    this.catService.getAll().subscribe({
      next: (data) => { this.categories = data },
      error: (error) => { console.log('error' + error) },
      complete: () => { },
    });

    // ---------------- [ Get All Brands ]
    this.brandService.getAll().subscribe({
      next: (data) => { this.brands = data },
      error: (error) => { console.log('error' + error) },
      complete: () => { },
    });

  }


  addWarranty() {

    const warrantyInput = this.fg.get('warranties')?.value as string;

    const [partName, duration] = warrantyInput.split('-');

    const newWarranty: Iwarranty = { partName, duration };

    this.warranties.push(newWarranty);

    this.fg.get('warranties')?.reset();
  }

  AssignImage(e: any) {
    this.selectedImage = e.target.files[0];
  }

  addImage() {
    if (this.selectedImage != null) {
      this.images.push(this.selectedImage);
      this.fg.get('images')?.reset();
      this.selectedImage = null;
    }
    else {
      console.log('there is no images choosed')
    }
  }

  //#region Old OnSubmit
  // OnSubmit(e: Event) {
  //   e.preventDefault();
  //   console.log(this.fg);
  //   console.log(this.product);
  //   if (this.fg.valid) {
  //     this.product.name = this.fg.get('name')?.value;
  //     this.product.description = this.fg.get('description')?.value;
  //     this.product.price = this.fg.get('price')?.value;
  //     this.product.condition = this.fg.get('condition')?.value;
  //     this.product.stockQuantity = this.fg.get('stockQuantity')?.value;
  //     this.product.discount = this.fg.get('discount')?.value;
  //     this.product.model = this.fg.get('model')?.value;
  //     this.product.color = this.fg.get('color')?.value;
  //     this.product.ProductDetailone = this.fg.get('ProductDetailone')?.value;
  //     this.product.ProductDetailtwo = this.fg.get('ProductDetailtwo')?.value;
  //     this.product.ProductDetailthree = this.fg.get('ProductDetailthree')?.value;
  //     this.product.ProductDetailfour = this.fg.get('ProductDetailfour')?.value;
  //     this.product.ProductDetailfive = this.fg.get('ProductDetailfive')?.value;
  //     this.product.ProductDetailsix = this.fg.get('ProductDetailsix')?.value;
  //     this.product.ProductDetailsiven = this.fg.get('ProductDetailsiven')?.value;
  //     this.product.categoryID = this.fg.get('categoryID')?.value;
  //     this.product.brandID = this.fg.get('brandID')?.value;
  //     // this.product.warranties =  this.product.warranties.push(newWarranty);
  //     //this.product.images = this.fg.get('images')?.value;
  //     console.log("pass valid");


  //     this.productId = this.activatedRoute.snapshot.paProductDetailtwos['id'];

  //     console.log('product to add :', this.product); // work and get data
  //     if (this.productId > 0) {
  //       //edit
  //       this.prodService.edit(this.productId, this.product).subscribe({
  //         next: (data) => this.router.navigate(['/adminproducts']),
  //         error: (error) => console.log('error' + error),
  //         complete: () => console.log("Successfully Add Product!"),
  //       });
  //     } else {
  //       //add
  //       this.prodService.add(this.product).subscribe({
  //         next: () => this.router.navigate(['/adminproducts']),
  //         error: (error) => console.log('error', JSON.stringify(error)),
  //         complete: () => console.log("Successfully Add Product!"),
  //       });
  //     }
  //   }
  //   else {
  //     // Log validation errors
  //     console.log('Form is invalid');
  //     // Log individual form controls' errors
  //     console.log('Form control errors:', this.fg.errors);

  //     // Log errors for warranties
  //     const warrantiesErrors = this.fg.get('warranties')?.errors;
  //     if (warrantiesErrors) {
  //       console.log('Warranties errors:', warrantiesErrors);
  //     }
  //     // Log errors for images
  //     const imagesErrors = this.fg.get('images')?.errors;
  //     if (imagesErrors) {
  //       console.log('Images errors:', imagesErrors);
  //     }
  //   }

  // }
  //#endregion

  OnSubmit(e: Event) {

    e.preventDefault();

    if (this.fg.valid) {

      //#region Assigning the form Data
      let formData = new FormData();
      formData.append('name', this.fg.get('name')?.value);
      formData.append('description', this.fg.get('description')?.value);
      formData.append('price', this.fg.get('price')?.value.toString());
      formData.append('condition', this.fg.get('condition')?.value.toString());
      formData.append('stockQuantity', this.fg.get('stockQuantity')?.value.toString());
      formData.append('discount', this.fg.get('discount')?.value.toString());
      formData.append('model', this.fg.get('model')?.value);
      formData.append('color', this.fg.get('color')?.value);
      formData.append('ProductDetailone', this.fg.get('ProductDetailone')?.value);
      formData.append('ProductDetailtwo', this.fg.get('ProductDetailtwo')?.value);
      formData.append('ProductDetailthree', this.fg.get('ProductDetailthree')?.value);
      formData.append('ProductDetailfour', this.fg.get('ProductDetailfour')?.value);
      formData.append('ProductDetailfive', this.fg.get('ProductDetailfive')?.value);
      formData.append('ProductDetailsix', this.fg.get('ProductDetailsix')?.value);
      formData.append('ProductDetailsiven', this.fg.get('ProductDetailsiven')?.value);
      formData.append('categoryID', this.fg.get('categoryID')?.value.toString());
      formData.append('brandID', this.fg.get('brandID')?.value.toString());

      for (let item of this.warranties) {
        formData.append(`warranties`, JSON.stringify(item));
      }

      for (let image of this.images) {
        formData.append(`images`, image, image.name);
      }
      //#endregion


      //#region Sending Data to API
      this.productapi.AddProduct(formData).subscribe({
        next: (d) => { console.log('Adding Product ...', d), this.router.navigate(['/admin/products']) },
        error: (e) => console.log('Error: ', e),
        complete: () => console.log('Added Product Successfully!')
      })
      //#endregion

    }

    else {
      console.log('Form is invalid');
    }

  }

  //#region Properties Getter
  // ---------------- [ name ]
  get nameRequired(): boolean | void { return this.fg.get('name')?.hasError('required'); }
  get nameValid(): boolean | void { return this.fg.get('name')?.valid; }
  get nameTouched(): boolean | void { return this.fg.get('name')?.touched; }

  // ---------------- [ description ]
  get descriptionRequired(): boolean | void { return this.fg.get('description')?.hasError('required'); }
  get descriptionValid(): boolean | void { return this.fg.get('description')?.valid; }
  get descriptionTouched(): boolean | void { return this.fg.get('description')?.touched; }

  // ---------------- [ price ]
  get priceRequired(): boolean | void { return this.fg.get('price')?.hasError('required'); }
  get priceValid(): boolean | void { return this.fg.get('price')?.valid; }
  get priceTouched(): boolean | void { return this.fg.get('price')?.touched; }

  // ---------------- [ condition ]
  get conditionRequired(): boolean | void { return this.fg.get('condition')?.hasError('required'); }
  get conditionValid(): boolean | void { return this.fg.get('condition')?.valid; }
  get conditionTouched(): boolean | void { return this.fg.get('condition')?.touched; }

  // ---------------- [ stockQuantity ]
  get stockQuantityRequired(): boolean | void { return this.fg.get('stockQuantity')?.hasError('required'); }
  get stockQuantityValid(): boolean | void { return this.fg.get('stockQuantity')?.valid; }
  get stockQuantityTouched(): boolean | void { return this.fg.get('stockQuantity')?.touched; }

  // ---------------- [ discount ]
  get discountRequired(): boolean | void { return this.fg.get('discount')?.hasError('required'); }
  get discountValid(): boolean | void { return this.fg.get('discount')?.valid; }
  get discountTouched(): boolean | void { return this.fg.get('discount')?.touched; }

  // ---------------- [ model ]
  get modelRequired(): boolean | void { return this.fg.get('model')?.hasError('required'); }
  get modelValid(): boolean | void { return this.fg.get('model')?.valid; }
  get modelTouched(): boolean | void { return this.fg.get('model')?.touched; }

  // ---------------- [ color ]
  get colorRequired(): boolean | void { return this.fg.get('color')?.hasError('required'); }
  get colorValid(): boolean | void { return this.fg.get('color')?.valid; }
  get colorTouched(): boolean | void { return this.fg.get('color')?.touched; }

  // ---------------- [ ProductDetailone ]
  get ProductDetailoneRequired(): boolean | void { return this.fg.get('ProductDetailone')?.hasError('required'); }
  get ProductDetailoneValid(): boolean | void { return this.fg.get('ProductDetailone')?.valid; }
  get ProductDetailoneTouched(): boolean | void { return this.fg.get('ProductDetailone')?.touched; }

  // ---------------- [ ProductDetailtwo ]
  get ProductDetailtwoRequired(): boolean | void { return this.fg.get('ProductDetailtwo')?.hasError('required'); }
  get ProductDetailtwoValid(): boolean | void { return this.fg.get('ProductDetailtwo')?.valid; }
  get ProductDetailtwoTouched(): boolean | void { return this.fg.get('ProductDetailtwo')?.touched; }

  // ---------------- [ ProductDetailthree ]
  get ProductDetailthreeRequired(): boolean | void { return this.fg.get('ProductDetailthree')?.hasError('required'); }
  get ProductDetailthreeValid(): boolean | void { return this.fg.get('ProductDetailthree')?.valid; }
  get ProductDetailthreeTouched(): boolean | void { return this.fg.get('ProductDetailthree')?.touched; }


  // ---------------- [ ProductDetailfour ]
  get ProductDetailfourRequired(): boolean | void { return this.fg.get('ProductDetailfour')?.hasError('required'); }
  get ProductDetailfourValid(): boolean | void { return this.fg.get('ProductDetailfour')?.valid; }
  get ProductDetailfourTouched(): boolean | void { return this.fg.get('ProductDetailfour')?.touched; }

  // ---------------- [ ProductDetailfive ]
  get ProductDetailfiveRequired(): boolean | void { return this.fg.get('ProductDetailfive')?.hasError('required'); }
  get ProductDetailfiveValid(): boolean | void { return this.fg.get('ProductDetailfive')?.valid; }
  get ProductDetailfiveTouched(): boolean | void { return this.fg.get('ProductDetailfive')?.touched; }

  // ---------------- [ ProductDetailsix ]
  get ProductDetailsixRequired(): boolean | void { return this.fg.get('ProductDetailsix')?.hasError('required'); }
  get ProductDetailsixValid(): boolean | void { return this.fg.get('ProductDetailsix')?.valid; }
  get ProductDetailsixTouched(): boolean | void { return this.fg.get('ProductDetailsix')?.touched; }

  // ---------------- [ ProductDetailsiven ]
  get ProductDetailsivenRequired(): boolean | void { return this.fg.get('ProductDetailsiven')?.hasError('required'); }
  get ProductDetailsivenValid(): boolean | void { return this.fg.get('ProductDetailsiven')?.valid; }
  get ProductDetailsivenTouched(): boolean | void { return this.fg.get('ProductDetailsiven')?.touched; }

  // ---------------- [ categoryID ]
  get categoryIDRequired(): boolean | void { return this.fg.get('categoryID')?.hasError('required'); }
  get categoryIDValid(): boolean | void { return this.fg.get('categoryID')?.valid; }
  get categoryIDTouched(): boolean | void { return this.fg.get('categoryID')?.touched; }

  // ---------------- [ brandID ]
  get brandIDRequired(): boolean | void { return this.fg.get('brandID')?.hasError('required'); }
  get brandIDValid(): boolean | void { return this.fg.get('brandID')?.valid; }
  get brandIDTouched(): boolean | void { return this.fg.get('brandID')?.touched; }

  // ---------------- [ warranties ]
  public get warrantiesRequired(): boolean | void { return this.fg.get('warranties')?.hasError('required'); }
  get warrantiesValid(): boolean | void { return this.fg.get('warranties')?.valid; }
  get warrantiesTouched(): boolean | void { return this.fg.get('warranties')?.touched; }

  // ---------------- [ images ]
  get imagesRequired(): boolean | void { return this.fg.get('images')?.hasError('required'); }
  get imagesValid(): boolean | void { return this.fg.get('images')?.valid; }
  get imagesTouched(): boolean | void { return this.fg.get('images')?.touched; }
  //#endregion

}
