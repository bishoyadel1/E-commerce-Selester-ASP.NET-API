import { Iproductadd } from './../../../../Interfaces/product/iproductadd';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Iproduct } from 'src/app/Interfaces/iproduct';
import { Iwarranty } from 'src/app/Interfaces/iwarranty';
import { Iproductreturn } from 'src/app/Interfaces/product/iproductreturn';
import { ProductlistService } from 'src/app/Services/productlist.service';

@Component({
  selector: 'app-edit-product-form',
  templateUrl: './edit-product-form.component.html',
  styleUrls: ['./edit-product-form.component.css']
})
export class EditProductFormComponent {

  fg !: FormGroup;

  selectedImage: File | null = null;

  //------------- [ get product data in case of edit]
  productById: Iproductreturn = {
    id: 0,
    name: '',
    description: '',
    price: 0,
    discount: 0,
    priceAfter: 0,
    condition: 0,
    stockQuantity: 0,
    model: '',
    color: '',
    productDetailone: '',
    productDetailtwo: '',
    productDetailthree: '',
    productDetailfour: '',
    productDetailfive: '',
    productDetailsix: '',
    productDetailsiven: '',
    categoryID: 0,
    categoryName: '',
    brandID: 0,
    brandName: '',
    warranties: [],
    images: [],
    avgRating: 0,
    avgRatingRounded: 0
  };

  productId: number = 0;
  images: File[] = [];
  warranties: Iwarranty[] = [];

  constructor(private fb: FormBuilder, private productapi: ProductlistService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {

    this.productId = this.activatedRoute.snapshot.params['id'];

    // Initializing the form group
    this.fg = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(100)]],
      price: [null, [Validators.required, Validators.min(1000), Validators.max(80000)]],
      condition: [null, [Validators.required, Validators.min(0), Validators.max(1)]],
      stockQuantity: [null, [Validators.required, Validators.min(0), Validators.max(100)]],
      discount: [null, [Validators.required, Validators.min(0), Validators.max(70)]],
      model: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      color: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      productDetailone: [null, [Validators.required, Validators.min(16), Validators.max(1000)]],
      productDetailtwo: [null, [Validators.required, Validators.min(8), Validators.max(64)]],
      productDetailthree: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      productDetailfour: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      productDetailfive: [null, [Validators.required]],
      productDetailsix: [null, [Validators.required, Validators.min(16), Validators.max(10000)]],
      productDetailsiven: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      categoryID: [null, [Validators.required, Validators.min(1)]],
      brandID: [null, [Validators.required]],
      warranties: [''],
      images: ['']
    });

    //------------- [ in case of edit declare fg with product data]
    this.productapi.GetProductByIdAdmin(this.productId).subscribe({
      next: (d) => {
        this.productById = d;
      },
      error: (e) => console.log('Unable to load data : ', e),
      complete: () => {

        console.log('Got Product Data Successfully!')
        console.log(this.productById)
        // in case we got data successfully assign it to form controls
        this.fg.patchValue({
          name: this.productById.name,
          description: this.productById.description,
          price: this.productById.price,
          condition: this.productById.condition,
          stockQuantity: this.productById.stockQuantity,
          discount: this.productById.discount,
          model: this.productById.model,
          color: this.productById.color,
          productDetailone: this.productById.productDetailone,
          productDetailtwo: this.productById.productDetailtwo,
          productDetailthree: this.productById.productDetailthree,
          productDetailfour: this.productById.productDetailfour,
          productDetailfive: this.productById.productDetailfive,
          productDetailsix: this.productById.productDetailsix,
          productDetailsiven: this.productById.productDetailsiven,
          categoryID: this.productById.categoryID,
          brandID: this.productById.brandID,
        });

      }
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


  OnSubmit(e: Event) {

    e.preventDefault();

    if (this.fg.valid) {
      let formData = new FormData();
      formData.append('id', this.productId.toString());
      formData.append('name', this.fg.get('name')?.value);
      formData.append('description', this.fg.get('description')?.value);
      formData.append('price', this.fg.get('price')?.value.toString());
      formData.append('condition', this.fg.get('condition')?.value.toString());
      formData.append('stockQuantity', this.fg.get('stockQuantity')?.value.toString());
      formData.append('discount', this.fg.get('discount')?.value.toString());
      formData.append('model', this.fg.get('model')?.value);
      formData.append('color', this.fg.get('color')?.value);
      formData.append('productDetailone', this.fg.get('productDetailone')?.value.toString());
      formData.append('productDetailtwo', this.fg.get('productDetailtwo')?.value.toString());
      formData.append('productDetailthree', this.fg.get('productDetailthree')?.value);
      formData.append('productDetailfour', this.fg.get('productDetailfour')?.value);
      formData.append('productDetailfive', this.fg.get('productDetailfive')?.value.toString());
      formData.append('productDetailsix', this.fg.get('productDetailsix')?.value.toString());
      formData.append('productDetailsiven', this.fg.get('productDetailsiven')?.value);
      formData.append('categoryID', this.fg.get('categoryID')?.value.toString());
      formData.append('brandID', this.fg.get('brandID')?.value.toString());


      for (let item of this.warranties) {
        formData.append(`warranties`, JSON.stringify(item));
      }

      for (let image of this.images) {
        formData.append(`images`, image, image.name);
      }

      this.productapi.EditProduct(formData).subscribe({
        next: (d) => {
          console.log('Editing Product ...', d)
          this.router.navigate(['/admin/products'])
        },
        error: (e) => console.log('Error: ', e),
        complete: () => console.log('Edit Product Successfully!')
      })

    }

    else {
      console.log('Form is invalid');
    }

  }




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

  // ---------------- [ productDetailone ]
  get productDetailoneRequired(): boolean | void { return this.fg.get('productDetailone')?.hasError('required'); }
  get productDetailoneValid(): boolean | void { return this.fg.get('productDetailone')?.valid; }
  get productDetailoneTouched(): boolean | void { return this.fg.get('productDetailone')?.touched; }

  // ---------------- [ productDetailtwo ]
  get productDetailtwoRequired(): boolean | void { return this.fg.get('productDetailtwo')?.hasError('required'); }
  get productDetailtwoValid(): boolean | void { return this.fg.get('productDetailtwo')?.valid; }
  get productDetailtwoTouched(): boolean | void { return this.fg.get('productDetailtwo')?.touched; }

  // ---------------- [ productDetailthree ]
  get productDetailthreeRequired(): boolean | void { return this.fg.get('productDetailthree')?.hasError('required'); }
  get productDetailthreeValid(): boolean | void { return this.fg.get('productDetailthree')?.valid; }
  get productDetailthreeTouched(): boolean | void { return this.fg.get('productDetailthree')?.touched; }


  // ---------------- [ productDetailfour ]
  get productDetailfourRequired(): boolean | void { return this.fg.get('productDetailfour')?.hasError('required'); }
  get productDetailfourValid(): boolean | void { return this.fg.get('productDetailfour')?.valid; }
  get productDetailfourTouched(): boolean | void { return this.fg.get('productDetailfour')?.touched; }

  // ---------------- [ productDetailfive ]
  get productDetailfiveRequired(): boolean | void { return this.fg.get('productDetailfive')?.hasError('required'); }
  get productDetailfiveValid(): boolean | void { return this.fg.get('productDetailfive')?.valid; }
  get productDetailfiveTouched(): boolean | void { return this.fg.get('productDetailfive')?.touched; }

  // ---------------- [ productDetailsix ]
  get productDetailsixRequired(): boolean | void { return this.fg.get('productDetailsix')?.hasError('required'); }
  get productDetailsixValid(): boolean | void { return this.fg.get('productDetailsix')?.valid; }
  get productDetailsixTouched(): boolean | void { return this.fg.get('productDetailsix')?.touched; }

  // ---------------- [ productDetailsiven ]
  get productDetailsivenRequired(): boolean | void { return this.fg.get('productDetailsiven')?.hasError('required'); }
  get productDetailsivenValid(): boolean | void { return this.fg.get('productDetailsiven')?.valid; }
  get productDetailsivenTouched(): boolean | void { return this.fg.get('productDetailsiven')?.touched; }

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


}
