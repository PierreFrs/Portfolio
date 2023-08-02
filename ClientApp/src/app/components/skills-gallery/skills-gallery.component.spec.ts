import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillsGalleryComponent } from './skills-gallery.component';

describe('SkillsGalleryComponent', () => {
  let component: SkillsGalleryComponent;
  let fixture: ComponentFixture<SkillsGalleryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SkillsGalleryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SkillsGalleryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
