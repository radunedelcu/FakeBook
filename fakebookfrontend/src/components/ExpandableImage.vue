<template>
  <div class="expandable-image" :class="{ expanded }" @click="expanded = true">

    <i v-if="expanded" class="close-button">

      <svg style="width:24px;height:24px" viewBox="0 0 24 24">

        <path fill="#666666"
          d="M19,6.41L17.59,5L12,10.59L6.41,5L5,6.41L10.59,12L5,17.59L6.41,19L12,13.41L17.59,19L19,17.59L13.41,12L19,6.41Z" />

      </svg>

    </i>
    <i v-else class="expand-button">
      <svg style="width:24px;height:24px" viewBox="0 0 24 24">
        <path fill="#000000"
          d="M10,21V19H6.41L10.91,14.5L9.5,13.09L5,17.59V14H3V21H10M14.5,10.91L19,6.41V10H21V3H14V5H17.59L13.09,9.5L14.5,10.91Z" />
      </svg>
    </i>

    <div class="popup row h-100">
      <div class="col-lg-8 col-12 my-auto">
        <div class="image-container">
          <img v-bind="$attrs" />
        </div>
      </div>
      <div v-if="expanded" class="col-lg-4 col-12 h-100">
          <div  class="h-100">
            <div class="bg-white p-4 h-100 position-relative">
              <div style="height:80%; overflow-y:scroll;flex-flow:column">
                      <div class="d-flex flex-start align-items-center" style="flex: 0 1 auto">
                        <img class="profileImage"
                        src="https://mdbcdn.b-cdn.net/img/Photos/Avatars/img%20(19).webp" alt="avatar"/>
                                <div class="info">
                                  <h6 class="fw-bold text-primary mb-1">Lily Coleman</h6>
                                  <p class="text-muted small mb-0">
                                    Shared publicly - Jan 2020
                                  </p>
                                </div>
                              </div>
                              
                              <p class="mt-3 mb-4 pb-2">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
                                tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,
                                quis nostrud exercitation ullamco laboris nisi ut aliquip consequat.
                              </p>
                              
                              <div class="small d-flex justify-content-start">
                                <a href="#!" class="d-flex align-items-center me-3">
                                  <i class="far fa-thumbs-up me-2"></i>
                                  <p class="mb-0">Like</p>
                                </a>
                                <a href="#!" class="d-flex align-items-center me-3">
                                  <i class="far fa-comment-dots me-2"></i>
                                  <p class="mb-0">Comment</p>
                                </a>
                                <a href="#!" class="d-flex align-items-center me-3">
                                  <i class="fas fa-share me-2"></i>
                                  <p class="mb-0">Share</p>
                                </a>
                              </div>
                              <div v-for="comment in getDummyComments()">
                                <span>{{comment}} COMMENT</span>
                              </div>
                            </div>
                            <div class=" py-3 border-0" style="flex: 1 1 auto;">
                              <div class="d-flex flex-start w-100">
                               
                                <div class="form-outline w-100">
                                  <textarea class="form-control" id="textAreaExample" rows="4"
                                  style="background: #fff;"></textarea>
                                  <label class="form-label" for="textAreaExample">Message</label>
                                </div>
                              </div>
                              <div class="d-flex justify-content-end">
                                <button type="button" class="btn btn-primary btn-sm">Post comment</button>
                                <button type="button" class="btn btn-outline-primary btn-sm">Cancel</button>
                              </div>
                            </div>
                          </div>
                      </div>
      </div>

    </div>


  </div>


</template>

<script scoped>
export default {
  inheritAttrs: false,
  data() {
    return {
      expanded: false
    }
  },
  methods:
  {
    closeImage(event) {
      this.expanded = false
      event.stopPropagation()
    },
    freezeVp(e) {
      e.preventDefault()
    },
    onExpandedImageClick(e) {
      e.stopPropagation()
      const image = this.cloned.querySelector('img')
      const imagePosition = this.getRenderedSize(image.width, image.height, image.naturalWidth, image.naturalHeight)
      if (
        (e.clientX < imagePosition.left) ||
        (e.clientX > imagePosition.right) ||
        (e.clientY < imagePosition.top) ||
        (e.clientY > imagePosition.bottom)
      ) {
        this.expanded = false
      }
    },
    getRenderedSize(cWidth, cHeight, oWidth, oHeight) {
      const oRatio = oWidth > oHeight
        ? oWidth / oHeight
        : oHeight / oWidth
      const width = oWidth >= oHeight
        ? oRatio * cHeight
        : cWidth
      const height = oHeight > oWidth
        ? oRatio * cWidth
        : cHeight
      const left = (this.cloned.clientWidth - width) / 2
      const right = left + width
      const top = (this.cloned.clientHeight - height) / 2
      const bottom = top + height
      return { left, top, right, bottom }
    },

    getDummyComments() {
      return [
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
      ]
    }
  },
  watch: {
    expanded(expanded) {
      this.$nextTick(() => {
        // Run this if when we're expanding the image
        if (expanded) {
          // Clone the entire expandable-image element
          this.cloned = this.$el.cloneNode(true)
          // Store a reference for the close button
          this.closeButtonRef = this.cloned.querySelector('.close-button')
          this.comments = this.cloned.querySelector('comments')
          // Call closeImage when the close button is clicked
          this.closeButtonRef.addEventListener('click', this.closeImage)
          // Add the cloned element into <body>
          document.body.appendChild(this.cloned)
          // Prevent the page from scrolling
          document.body.style.overflow = 'hidden'

          // Show the cloned element
          this.cloned.style.opacity = 1

        } else {
          // This section will run when the image is closing
          // Hide the expanded image
          this.cloned.style.opacity = 0
          setTimeout(() => {
            // Then, remove the click event listener from the close button
            this.closeButtonRef.removeEventListener('click', this.closeImage)
            // Remove the cloned element and the references
            this.cloned.remove()
            this.cloned = null
            this.closeButtonRef = null
            // Re-enable the scrolling
            document.body.style.overflow = 'auto'
          }, 250)
        }
      })
    }
  }

}
</script>

<style>
.expandable-image {
  position: relative;
  transition: 0.25s opacity;
  display: flex !important;
}

body>.expandable-image.expanded {
  position: fixed;
  z-index: 999999;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: black;
  display: flex;
  align-items: center;
  opacity: 0;
  padding-bottom: 0 !important;
  cursor: default;
}

body>.expandable-image.expanded>img {
  width: 100%;
  max-width: 1200px;
  max-height: 100%;
  object-fit: contain;
  margin-right: auto;
}

body>.expandable-image.expanded>.close-button {
  display: block;
}

.close-button {
  position: fixed;
  top: 10px;
  right: 10px;
  display: none;
  cursor: pointer;
}

svg {
  filter: drop-shadow(1px 1px 1px rgba(0, 0, 0, 0.5));
}

svg path {
  fill: #FFF;
}

.expand-button {
  position: absolute;
  z-index: 999;
  right: 10px;
  top: 10px;
  padding: 0px;
  align-items: center;
  justify-content: center;
  padding: 3px;
  opacity: 0;
  transition: 0.2s opacity;
}

.expandable-image:hover {
  cursor: pointer;
  opacity: 1;
}

.expand-button svg {
  width: 20px;
  height: 20px;
}

.expand-button path {
  fill: #FFF;
}

.expandable-image img {
  width: 100%;
}

.profileImage {
  height: 64px;
  width: 64px !important;
  border-radius: 50%;
}

.info {
  margin-left: 10px;
}

.card {
  border-radius: 0 !important;
}

.image-container {
  float: left;
}

.image-container img {
  width: 100%;
}

.popup {
  display: flex;
}

.comments {
  float: right;
}

.test {
  background-color: red;
}
</style>
